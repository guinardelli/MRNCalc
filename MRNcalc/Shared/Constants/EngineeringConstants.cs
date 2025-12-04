namespace MRNcalc.Shared.Constants;

/// <summary>
/// Constantes de engenharia utilizadas nos cálculos de dimensionamento estrutural.
/// </summary>
public static class EngineeringConstants
{
    /// <summary>
    /// Coeficiente de redução da resistência do concreto no bloco retangular simplificado (αc).
    /// Valor padrão conforme NBR 6118:2023.
    /// </summary>
    public const double AlphaC = 0.85;

    /// <summary>
    /// Fator de conversão de tonelada-força (tf) para kilonewton (kN).
    /// 1 tf = 9,80665 kN (aceleração da gravidade padrão).
    /// </summary>
    public const double ConversaoTfParaKn = 9.80665;

    /// <summary>
    /// Limite relativo padrão da linha neutra (x/d) para dimensionamento.
    /// Valor típico para CA-50 e concretos usuais conforme NBR 6118:2023.
    /// </summary>
    public const double LimiteRelativoPadrao = 0.45;

    /// <summary>
    /// Limite relativo da linha neutra para aços de alta resistência (fy ≥ 600 MPa).
    /// Valor reduzido conforme Eurocode 2.
    /// </summary>
    public const double LimiteRelativoAcoAltaResistencia = 0.40;

    /// <summary>
    /// Fator de conversão de megapascal (MPa) para kilonewton por metro quadrado (kN/m²).
    /// 1 MPa = 1000 kN/m².
    /// </summary>
    public const double ConversaoMpaParaKnm2 = 1000.0;

    /// <summary>
    /// Fator de conversão de metro quadrado (m²) para centímetro quadrado (cm²).
    /// 1 m² = 10.000 cm² = 1e4 cm².
    /// </summary>
    public const double ConversaoM2ParaCm2 = 1e4;

    /// <summary>
    /// Fator de conversão de centímetro (cm) para metro (m).
    /// 1 m = 100 cm.
    /// </summary>
    public const double ConversaoCmParaM = 100.0;

    /// <summary>
    /// Limite mínimo para profundidade relativa da linha neutra.
    /// </summary>
    public const double LimiteRelativoMinimo = 0.0;

    /// <summary>
    /// Limite máximo para profundidade relativa da linha neutra.
    /// </summary>
    public const double LimiteRelativoMaximo = 0.9;
}

