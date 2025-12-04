using System;
using System.Globalization;
using System.Windows.Forms;

namespace MRNcalc;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnCalcular_Click(object sender, EventArgs e)
    {
        try
        {
            // Leitura e validação básica dos dados de entrada
            double larguraCm = ParseDouble(txtLargura.Text, "Largura");
            double alturaCm = ParseDouble(txtAltura.Text, "Altura");
            double momentoTfm = ParseDouble(txtMomento.Text, "Momento característico");
            double distFacesArmadurasCm = ParseDouble(txtDistArmaduras.Text, "Distância entre faces e armaduras");

            if (larguraCm <= 0 || alturaCm <= 0)
                throw new ApplicationException("Largura e altura da seção devem ser maiores que zero.");

            if (distFacesArmadurasCm <= 0 || distFacesArmadurasCm >= alturaCm / 2.0)
                throw new ApplicationException("A distância entre faces e armaduras deve ser positiva e menor que metade da altura da seção.");

            if (cmbFck.SelectedItem is null)
                throw new ApplicationException("Selecione um valor de fck.");
            if (cmbFyk.SelectedItem is null)
                throw new ApplicationException("Selecione um valor de fy.");

            double fck = ParseDouble(cmbFck.SelectedItem.ToString()!, "fck"); // MPa
            double fyk = ParseDouble(cmbFyk.SelectedItem.ToString()!, "fy");  // MPa

            double gammaF = ParseDouble(txtGammaF.Text, "γf");
            double gammaC = ParseDouble(txtGammaC.Text, "γc");
            double gammaS = ParseDouble(txtGammaS.Text, "γs");
            double ktc = ParseDouble(txtKtc.Text, "ktc");
            double redistPercent = ParseDouble(txtRedist.Text, "Redistr. de momentos");

            if (gammaF <= 0 || gammaC <= 0 || gammaS <= 0 || ktc <= 0)
                throw new ApplicationException("Os fatores de ponderação (γf, γc, γs, ktc) devem ser maiores que zero.");

            // Norma selecionada (a princípio apenas altera o limite automático de x/d)
            bool usaNBR6118 = rdbNBR6118.Checked;

            // Limite relativo da linha neutra
            double limiteRelativo;
            if (rdbLimiteAutomatico.Checked)
            {
                // Valores típicos de projeto (simplificados):
                // NBR 6118: 0,45 para CA-50 / concretos usuais
                // Eurocode 2: 0,45 (aproximação para aço classe B)
                limiteRelativo = 0.45;

                // Ajuste opcional bem simples: se aço de maior resistência, reduzir um pouco o limite
                if (!usaNBR6118 && fyk >= 600)
                    limiteRelativo = 0.40;
            }
            else
            {
                limiteRelativo = ParseDouble(txtLimiteRelativo.Text, "Limite relativo (x/d)");
            }

            if (limiteRelativo <= 0 || limiteRelativo >= 0.9)
                throw new ApplicationException("O limite relativo da linha neutra deve estar entre 0 e 0,9.");

            // Conversões de unidades
            // Geometria em metros
            double b = larguraCm / 100.0;              // m
            double h = alturaCm / 100.0;               // m
            double dLinha = distFacesArmadurasCm / 100.0; // m (profundidade da armadura comprimida)
            double d = h - dLinha;                     // m (profundidade da armadura tracionada)

            if (d <= 0)
                throw new ApplicationException("Profundidade útil d inválida. Verifique altura da seção e distâncias às armaduras.");

            // Momento característico (tf·m) -> kN·m
            // 1 tf ≈ 9,80665 kN
            double Mk_kNm = momentoTfm * 9.80665;

            // Momento de cálculo (ponderado + longa duração + redistr.)
            double fatorRedist = 1.0 - redistPercent / 100.0;
            if (fatorRedist <= 0)
                throw new ApplicationException("O fator de redistribuição de momentos deve resultar em valor positivo.");

            double Msd_kNm = Mk_kNm * gammaF * ktc * fatorRedist;

            // Resistências de cálculo (em kN/m²)
            // 1 MPa = 1000 kN/m²
            double fcd = fck * 1000.0 / gammaC;          // kN/m²
            double fyd = fyk * 1000.0 / gammaS;          // kN/m²

            // Bloco retangular simplificado: Msd = αc * fcd * b * x * (d - 0,4 x)
            const double alphaC = 0.85;

            // Cálculo da altura da linha neutra x (simplificação de seção retangular)
            double a = -0.4 * alphaC * fcd * b;
            double bq = alphaC * fcd * b * d;
            double c = -Msd_kNm;

            double delta = bq * bq - 4.0 * a * c;
            if (delta < 0)
                throw new ApplicationException("Não foi possível encontrar uma solução para a linha neutra (equação sem raiz real). Verifique valores de entrada.");

            double sqrtDelta = Math.Sqrt(delta);

            // Duas raízes; escolhemos a fisicamente admissível (0 < x < d)
            double x1 = (-bq + sqrtDelta) / (2.0 * a);
            double x2 = (-bq - sqrtDelta) / (2.0 * a);

            double x = EscolherRaizFisica(x1, x2, d);
            if (double.IsNaN(x))
                throw new ApplicationException("Altura da linha neutra fora do intervalo físico (0 < x < d). Verifique os dados.");

            double profRelativa = x / d;

            double As_m2;
            double AsLinha_m2;

            // Verifica se é necessária armadura dupla (x/d > limite)
            if (profRelativa <= limiteRelativo)
            {
                // Seção simples armada
                double z = d - 0.4 * x;
                As_m2 = Msd_kNm / (fyd * z);
                AsLinha_m2 = 0.0;
            }
            else
            {
                // Seção duplamente armada
                double xLim = limiteRelativo * d;
                double zLim = d - 0.4 * xLim;

                // Momento máximo absorvido pela parte comprimida (até xLim)
                double Mlim_kNm = alphaC * fcd * b * xLim * zLim;

                if (Mlim_kNm <= 0 || Mlim_kNm > Msd_kNm)
                    throw new ApplicationException("Limite de linha neutra inconsistente para a combinação de carregamento e materiais.");

                // Armadura tracionada correspondente ao momento Mlim
                double As1_m2 = Mlim_kNm / (fyd * zLim);

                // Momento excedente absorvido pelo par As / As'
                double deltaM_kNm = Msd_kNm - Mlim_kNm;

                double zDuple = d - dLinha; // braço de alavanca entre As e As'
                if (zDuple <= 0)
                    throw new ApplicationException("Braço de alavanca entre armaduras negativo. Verifique distâncias às armaduras.");

                AsLinha_m2 = deltaM_kNm / (fyd * zDuple);
                As_m2 = As1_m2 + AsLinha_m2;

                // Para fins de exibição, adotamos x limitado
                x = xLim;
                profRelativa = x / d;
            }

            if (As_m2 <= 0)
                throw new ApplicationException("Área de aço tracionada calculada não positiva. Verifique dados de entrada.");

            // Conversão para cm²
            double As_cm2 = As_m2 * 1e4;       // 1 m² = 10⁴ cm²
            double AsLinha_cm2 = AsLinha_m2 * 1e4;
            double x_cm = x * 100.0;

            // Exibição dos resultados
            txtAs.Text = As_cm2.ToString("0.###", CultureInfo.InvariantCulture);
            txtAsLinha.Text = AsLinha_cm2 > 0 ? AsLinha_cm2.ToString("0.###", CultureInfo.InvariantCulture) : "0";
            txtProfRelativa.Text = profRelativa.ToString("0.###", CultureInfo.InvariantCulture);
            txtProfLinhaNeutra.Text = x_cm.ToString("0.###", CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro no cálculo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void rdbLimiteDefinir_CheckedChanged(object sender, EventArgs e)
    {
        txtLimiteRelativo.Enabled = rdbLimiteDefinir.Checked;
    }

    /// <summary>
    /// Faz o parse de um texto para double usando cultura invariável, aceitando vírgula ou ponto.
    /// </summary>
    private static double ParseDouble(string texto, string nomeCampo)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ApplicationException($"Informe um valor numérico para '{nomeCampo}'.");

        texto = texto.Trim().Replace(",", ".");
        if (!double.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
            throw new ApplicationException($"Valor inválido para '{nomeCampo}'.");

        return valor;
    }

    /// <summary>
    /// Escolhe a raiz fisicamente admissível (0 &lt; x &lt; d) entre duas soluções.
    /// </summary>
    private static double EscolherRaizFisica(double x1, double x2, double d)
    {
        bool RaizValida(double x) => x > 0 && x < d && !double.IsNaN(x) && !double.IsInfinity(x);

        if (RaizValida(x1) && !RaizValida(x2))
            return x1;
        if (RaizValida(x2) && !RaizValida(x1))
            return x2;
        if (RaizValida(x1) && RaizValida(x2))
            return Math.Min(x1, x2); // escolhe a menor positiva

        return double.NaN;
    }
}
