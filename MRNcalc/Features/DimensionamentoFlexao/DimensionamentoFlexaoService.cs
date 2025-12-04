using MRNcalc.Shared.Constants;
using MRNcalc.Shared.Helpers;

namespace MRNcalc.Features.DimensionamentoFlexao;

/// <summary>
/// Serviço responsável pelos cálculos de dimensionamento à flexão simples.
/// Implementa a lógica de engenharia conforme NBR 6118:2023.
/// Baseado no método do momento reduzido.
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

        // Geometria em centímetros (como no código de referência)
        double b = input.LarguraCm;              // cm
        double h = input.AlturaCm;                // cm
        double dLinha = input.DistFacesArmadurasCm; // cm
        double d = h - dLinha;                    // cm

        if (d <= 0)
            throw new ArgumentException("Profundidade útil d inválida. Verifique altura da seção e distâncias às armaduras.");

        // Parâmetros do diagrama retangular conforme NBR 6118
        double alamb;  // λ (lambda)
        double alfac;  // αc (alpha c)
        double eu;     // εu (deformação última do concreto)
        
        if (input.Fck <= 50)
        {
            alamb = 0.8;
            alfac = 0.85;
            eu = 3.5;
        }
        else
        {
            alamb = 0.8 - (input.Fck - 50) / 400;
            alfac = 0.85 * (1 - (input.Fck - 50) / 200);
            double a = (90 - input.Fck) / 100;
            eu = 2.6 + 35 * Math.Pow(a, 4);
        }

        // Determinar limite relativo da linha neutra (qlim)
        double qlim;
        if (!input.LimiteRelativoAutomatico && input.LimiteRelativo.HasValue)
        {
            qlim = input.LimiteRelativo.Value;
        }
        else
        {
            // Cálculo automático baseado no coeficiente de redistribuição (bduct)
            // bduct = 1 - RedistPercent/100 (coeficiente de redistribuição)
            double bduct = 1.0 - input.RedistPercent / 100.0;
            
            if (input.Fck <= 50)
            {
                qlim = 0.8 * bduct - 0.35;
            }
            else
            {
                qlim = 0.8 * bduct - 0.45;
            }
        }

        if (qlim <= 0 || qlim >= 0.9)
            throw new ArgumentException($"O limite relativo da linha neutra deve estar entre 0 e 0,9. Valor calculado: {qlim:F3}");

        // Conversão de unidades para kN e cm (como no código de referência)
        // Momento característico: tf·m -> kN·m -> kN·cm
        double Mk_kNm = input.MomentoTfm * EngineeringConstants.ConversaoTfParaKn;
        double Mk_kNcm = Mk_kNm * 100.0; // 1 m = 100 cm

        // Resistências em kN/cm² (conforme código de referência)
        // fck e fyk são convertidos de MPa para kN/cm² (dividindo por 10)
        double fck_kNcm2 = input.Fck / 10.0;
        double fyk_kNcm2 = input.Fyk / 10.0;

        // Resistências de cálculo
        // Nota: O código de referência não aplica ktc explicitamente na resistência do concreto
        // O ktc pode ser usado em outros contextos, mas aqui seguimos o código de referência
        double fcd = fck_kNcm2 / input.GammaC;  // kN/cm²
        double tcd = alfac * fcd;  // Tensão de cálculo do concreto (já com alfac aplicado)
        double fyd = fyk_kNcm2 / input.GammaS;  // kN/cm²

        // Momento de cálculo
        double Md_kNcm = input.GammaF * Mk_kNcm;

        // Parâmetro geométrico
        double delta = dLinha / d;

        // Momento limite reduzido
        double amilim = alamb * qlim * (1 - 0.5 * alamb * qlim);

        // Momento reduzido solicitante
        double ami = Md_kNcm / (b * d * d * tcd);

        double As_cm2;
        double AsLinha_cm2;
        double x_cm;
        double profRelativa;
        bool armaduraDupla = false;

        if (ami <= amilim)
        {
            // Armadura simples
            double qsi = (1 - Math.Sqrt(1 - 2 * ami)) / alamb;
            As_cm2 = alamb * qsi * b * d * tcd / fyd;
            AsLinha_cm2 = 0.0;
            x_cm = qsi * d;
            profRelativa = qsi;
        }
        else
        {
            // Armadura dupla
            armaduraDupla = true;

            // Evitando armadura dupla no domínio 2
            double qsia = eu / (eu + 10);
            if (qlim < qsia)
            {
                throw new InvalidOperationException("Resultou armadura dupla no domínio 2. Aumente as dimensões da seção transversal.");
            }

            // Eliminando o caso em que qlim <= delta
            // Se isto ocorrer, a armadura de compressão estará tracionada
            if (qlim <= delta)
            {
                throw new InvalidOperationException("Aumente as dimensões da seção transversal.");
            }

            // Deformação da armadura de compressão
            double esl = eu * (qlim - delta) / qlim;
            esl = esl / 1000.0; // Convertendo de % para decimal

            // Tensão na armadura de compressão
            double tsl = CalcularTensaoAco(esl, fyd, 21000.0); // Es = 210 GPa = 21000 kN/cm²

            AsLinha_cm2 = (ami - amilim) * b * d * tcd / ((1 - delta) * tsl);
            As_cm2 = (alamb * qlim + (ami - amilim) / (1 - delta)) * b * d * tcd / fyd;

            // Para fins de exibição, adotamos x limitado
            x_cm = qlim * d;
            profRelativa = qlim;
        }

        if (As_cm2 <= 0)
            throw new InvalidOperationException("Área de aço tracionada calculada não positiva. Verifique dados de entrada.");

        // Verificação de armadura mínima
        // fyd já está em kN/cm², mas a fórmula espera em MPa, então convertemos de volta
        double fyd_MPa = fyd * 10.0; // kN/cm² -> MPa (1 MPa = 0.1 kN/cm², então multiplicamos por 10)
        double asmin = CalcularArmaduraMinima(input.Fck, fyd_MPa, b, h);
        if (As_cm2 < asmin)
        {
            As_cm2 = asmin;
        }

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
    /// Calcula a tensão no aço baseada na deformação.
    /// </summary>
    private static double CalcularTensaoAco(double esl, double fyd, double es)
    {
        // Trabalhando com deformação positiva
        double ess = Math.Abs(esl);
        double eyd = fyd / es;
        
        double tsl;
        if (ess < eyd)
        {
            tsl = es * ess;
        }
        else
        {
            tsl = fyd;
        }

        // Trocando o sinal se necessário
        if (esl < 0)
        {
            tsl = -tsl;
        }

        return tsl;
    }

    /// <summary>
    /// Calcula a armadura mínima conforme NBR 6118.
    /// </summary>
    private static double CalcularArmaduraMinima(double fck, double fyd, double b, double h)
    {
        double romin;
        double a = 2.0 / 3.0;

        if (fck <= 50)
        {
            romin = 0.078 * Math.Pow(fck, a) / fyd;
        }
        else
        {
            romin = 0.5512 * Math.Log(1 + 0.11 * fck) / fyd;
        }

        if (romin < 0.0015)
        {
            romin = 0.0015;
        }

        return romin * b * h;
    }

    /// <summary>
    /// Valida se um valor é maior que zero.
    /// </summary>
    private static void ValidarMaiorQueZero(double valor, string nomeCampo)
    {
        ValidationHelper.ValidarMaiorQueZero(valor, nomeCampo);
    }
}
