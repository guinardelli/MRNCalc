namespace MRNcalc.Features.DimensionamentoFlexao;

/// <summary>
/// Modelo de entrada para o cálculo de dimensionamento à flexão simples.
/// </summary>
public class DimensionamentoFlexaoInput
{
    /// <summary>
    /// Largura da seção transversal em centímetros.
    /// </summary>
    public double LarguraCm { get; set; }

    /// <summary>
    /// Altura da seção transversal em centímetros.
    /// </summary>
    public double AlturaCm { get; set; }

    /// <summary>
    /// Momento característico em tonelada-força metro (tf·m).
    /// </summary>
    public double MomentoTfm { get; set; }

    /// <summary>
    /// Distância entre faces e armaduras em centímetros.
    /// </summary>
    public double DistFacesArmadurasCm { get; set; }

    /// <summary>
    /// Resistência característica do concreto à compressão em megapascal (MPa).
    /// </summary>
    public double Fck { get; set; }

    /// <summary>
    /// Resistência característica do aço ao escoamento em megapascal (MPa).
    /// </summary>
    public double Fyk { get; set; }

    /// <summary>
    /// Coeficiente de ponderação das ações (γf).
    /// </summary>
    public double GammaF { get; set; }

    /// <summary>
    /// Coeficiente de ponderação do concreto (γc).
    /// </summary>
    public double GammaC { get; set; }

    /// <summary>
    /// Coeficiente de ponderação do aço (γs).
    /// </summary>
    public double GammaS { get; set; }

    /// <summary>
    /// Coeficiente de longa duração (ktc).
    /// </summary>
    public double Ktc { get; set; }

    /// <summary>
    /// Percentual de redistribuição de momentos (%).
    /// </summary>
    public double RedistPercent { get; set; }

    /// <summary>
    /// Limite relativo da linha neutra (x/d). Usado quando não é automático.
    /// </summary>
    public double? LimiteRelativo { get; set; }

    /// <summary>
    /// Indica se o limite relativo deve ser calculado automaticamente.
    /// </summary>
    public bool LimiteRelativoAutomatico { get; set; }
}

/// <summary>
/// Modelo de saída com os resultados do dimensionamento à flexão simples.
/// </summary>
public class DimensionamentoFlexaoOutput
{
    /// <summary>
    /// Área de aço tracionada em centímetros quadrados (cm²).
    /// </summary>
    public double AsCm2 { get; set; }

    /// <summary>
    /// Área de aço comprimida em centímetros quadrados (cm²).
    /// </summary>
    public double AsLinhaCm2 { get; set; }

    /// <summary>
    /// Profundidade relativa da linha neutra (x/d).
    /// </summary>
    public double ProfRelativa { get; set; }

    /// <summary>
    /// Profundidade da linha neutra em centímetros (cm).
    /// </summary>
    public double ProfLinhaNeutraCm { get; set; }

    /// <summary>
    /// Indica se foi necessária armadura dupla.
    /// </summary>
    public bool ArmaduraDupla { get; set; }
}

