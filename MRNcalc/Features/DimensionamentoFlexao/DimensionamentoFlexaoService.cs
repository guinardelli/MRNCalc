using MRNcalc.Shared.Constants;
using MRNcalc.Shared.Helpers;

namespace MRNcalc.Features.DimensionamentoFlexao;

/// <summary>
/// Serviço responsável pelos cálculos de dimensionamento à flexão simples.
/// Implementa a lógica de engenharia conforme NBR 6118:2023 e Eurocode 2.
/// </summary>
public class DimensionamentoFlexaoService
{
    /// <summary>
    /// Calcula o dimensionamento à flexão simples conforme os parâmetros de entrada.
    /// </summary>
    /// <param name="input">Parâmetros de entrada do dimensionamento.</param>
    /// <returns>Resultados do dimensionamento.</returns>
    /// <exception cref="ArgumentException">Lançada quando os dados de entrada são inválidos.</exception>
    /// <exception cref="InvalidOperationException">Lançada quando não é possível resolver o cálculo.</exception>
    public DimensionamentoFlexaoOutput Calcular(DimensionamentoFlexaoInput input)
    {
        // Validações básicas
        ValidarMaiorQueZero(input.LarguraCm, "Largura");
        ValidarMaiorQueZero(input.AlturaCm, "Altura");
        ValidarMaiorQueZero(input.DistFacesArmadurasCm, "Distância entre faces e armaduras");
        ValidarMaiorQueZero(input.GammaF, "γf");
        ValidarMaiorQueZero(input.GammaC, "γc");
        ValidarMaiorQueZero(input.GammaS, "γs");
        ValidarMaiorQueZero(input.Ktc, "ktc");

        if (input.DistFacesArmadurasCm >= input.AlturaCm / 2.0)
            throw new ArgumentException("A distância entre faces e armaduras deve ser menor que metade da altura da seção.");

        // Determinar limite relativo da linha neutra
        double limiteRelativo = DeterminarLimiteRelativo(input);

        if (limiteRelativo <= EngineeringConstants.LimiteRelativoMinimo || 
            limiteRelativo >= EngineeringConstants.LimiteRelativoMaximo)
            throw new ArgumentException($"O limite relativo da linha neutra deve estar entre {EngineeringConstants.LimiteRelativoMinimo} e {EngineeringConstants.LimiteRelativoMaximo}.");

        // Conversões de unidades
        double b = input.LarguraCm / EngineeringConstants.ConversaoCmParaM;              // m
        double h = input.AlturaCm / EngineeringConstants.ConversaoCmParaM;                 // m
        double dLinha = input.DistFacesArmadurasCm / EngineeringConstants.ConversaoCmParaM; // m
        double d = h - dLinha;                                                             // m

        if (d <= 0)
            throw new ArgumentException("Profundidade útil d inválida. Verifique altura da seção e distâncias às armaduras.");

        // Momento característico (tf·m) -> kN·m
        double Mk_kNm = input.MomentoTfm * EngineeringConstants.ConversaoTfParaKn;

        // Momento de cálculo (ponderado + longa duração + redistr.)
        double fatorRedist = 1.0 - input.RedistPercent / 100.0;
        if (fatorRedist <= 0)
            throw new ArgumentException("O fator de redistribuição de momentos deve resultar em valor positivo.");

        double Msd_kNm = Mk_kNm * input.GammaF * input.Ktc * fatorRedist;

        // Resistências de cálculo (em kN/m²)
        double fcd = input.Fck * EngineeringConstants.ConversaoMpaParaKnm2 / input.GammaC;  // kN/m²
        double fyd = input.Fyk * EngineeringConstants.ConversaoMpaParaKnm2 / input.GammaS;  // kN/m²

        // Cálculo da altura da linha neutra x
        double x = CalcularAlturaLinhaNeutra(b, d, fcd, Msd_kNm);

        double profRelativa = x / d;

        double As_m2;
        double AsLinha_m2;
        bool armaduraDupla = false;

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
            armaduraDupla = true;
            double xLim = limiteRelativo * d;
            double zLim = d - 0.4 * xLim;

            // Momento máximo absorvido pela parte comprimida (até xLim)
            double Mlim_kNm = EngineeringConstants.AlphaC * fcd * b * xLim * zLim;

            if (Mlim_kNm <= 0 || Mlim_kNm > Msd_kNm)
                throw new InvalidOperationException("Limite de linha neutra inconsistente para a combinação de carregamento e materiais.");

            // Armadura tracionada correspondente ao momento Mlim
            double As1_m2 = Mlim_kNm / (fyd * zLim);

            // Momento excedente absorvido pelo par As / As'
            double deltaM_kNm = Msd_kNm - Mlim_kNm;

            double zDuple = d - dLinha; // braço de alavanca entre As e As'
            if (zDuple <= 0)
                throw new InvalidOperationException("Braço de alavanca entre armaduras negativo. Verifique distâncias às armaduras.");

            AsLinha_m2 = deltaM_kNm / (fyd * zDuple);
            As_m2 = As1_m2 + AsLinha_m2;

            // Para fins de exibição, adotamos x limitado
            x = xLim;
            profRelativa = x / d;
        }

        if (As_m2 <= 0)
            throw new InvalidOperationException("Área de aço tracionada calculada não positiva. Verifique dados de entrada.");

        // Conversão para cm²
        double As_cm2 = As_m2 * EngineeringConstants.ConversaoM2ParaCm2;
        double AsLinha_cm2 = AsLinha_m2 * EngineeringConstants.ConversaoM2ParaCm2;
        double x_cm = x * EngineeringConstants.ConversaoCmParaM;

        return new DimensionamentoFlexaoOutput
        {
            AsCm2 = As_cm2,
            AsLinhaCm2 = AsLinha_cm2,
            ProfRelativa = profRelativa,
            ProfLinhaNeutraCm = x_cm,
            ArmaduraDupla = armaduraDupla
        };
    }

    /// <summary>
    /// Determina o limite relativo da linha neutra baseado nas configurações de entrada.
    /// </summary>
    private double DeterminarLimiteRelativo(DimensionamentoFlexaoInput input)
    {
        if (!input.LimiteRelativoAutomatico && input.LimiteRelativo.HasValue)
        {
            return input.LimiteRelativo.Value;
        }

        // Valores típicos de projeto (simplificados):
        // NBR 6118: 0,45 para CA-50 / concretos usuais
        // Eurocode 2: 0,45 (aproximação para aço classe B)
        double limiteRelativo = EngineeringConstants.LimiteRelativoPadrao;

        // Ajuste opcional bem simples: se aço de maior resistência, reduzir um pouco o limite
        if (!input.UsaNBR6118 && input.Fyk >= 600)
        {
            limiteRelativo = EngineeringConstants.LimiteRelativoAcoAltaResistencia;
        }

        return limiteRelativo;
    }

    /// <summary>
    /// Calcula a altura da linha neutra x resolvendo a equação do bloco retangular simplificado.
    /// Msd = αc * fcd * b * x * (d - 0,4 x)
    /// </summary>
    private double CalcularAlturaLinhaNeutra(double b, double d, double fcd, double Msd_kNm)
    {
        // Equação: Msd = αc * fcd * b * x * (d - 0,4 x)
        // Rearranjando: -0,4 * αc * fcd * b * x² + αc * fcd * b * d * x - Msd = 0
        // Forma: a*x² + b*x + c = 0
        double a = -0.4 * EngineeringConstants.AlphaC * fcd * b;
        double bq = EngineeringConstants.AlphaC * fcd * b * d;
        double c = -Msd_kNm;

        double delta = bq * bq - 4.0 * a * c;
        if (delta < 0)
            throw new InvalidOperationException("Não foi possível encontrar uma solução para a linha neutra (equação sem raiz real). Verifique valores de entrada.");

        double sqrtDelta = Math.Sqrt(delta);

        // Duas raízes; escolhemos a fisicamente admissível (0 < x < d)
        double x1 = (-bq + sqrtDelta) / (2.0 * a);
        double x2 = (-bq - sqrtDelta) / (2.0 * a);

        double x = EscolherRaizFisica(x1, x2, d);
        if (double.IsNaN(x))
            throw new InvalidOperationException("Altura da linha neutra fora do intervalo físico (0 < x < d). Verifique os dados.");

        return x;
    }

    /// <summary>
    /// Escolhe a raiz fisicamente admissível (0 < x < d) entre duas soluções.
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

    /// <summary>
    /// Valida se um valor é maior que zero.
    /// </summary>
    private static void ValidarMaiorQueZero(double valor, string nomeCampo)
    {
        ValidationHelper.ValidarMaiorQueZero(valor, nomeCampo);
    }
}

