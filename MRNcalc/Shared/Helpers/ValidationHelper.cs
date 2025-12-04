using System.Globalization;

namespace MRNcalc.Shared.Helpers;

/// <summary>
/// Classe auxiliar para validação e conversão de valores numéricos.
/// </summary>
public static class ValidationHelper
{
    /// <summary>
    /// Faz o parse de um texto para double usando cultura invariável, aceitando vírgula ou ponto.
    /// </summary>
    /// <param name="texto">Texto a ser convertido.</param>
    /// <param name="nomeCampo">Nome do campo para mensagens de erro.</param>
    /// <returns>Valor convertido.</returns>
    /// <exception cref="ArgumentException">Lançada quando o texto é vazio ou inválido.</exception>
    public static double ParseDouble(string texto, string nomeCampo)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException($"Informe um valor numérico para '{nomeCampo}'.", nameof(texto));

        texto = texto.Trim().Replace(",", ".");
        if (!double.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
            throw new ArgumentException($"Valor inválido para '{nomeCampo}'.", nameof(texto));

        return valor;
    }

    /// <summary>
    /// Valida se um valor é maior que zero.
    /// </summary>
    /// <param name="valor">Valor a ser validado.</param>
    /// <param name="nomeCampo">Nome do campo para mensagens de erro.</param>
    /// <exception cref="ArgumentException">Lançada quando o valor é menor ou igual a zero.</exception>
    public static void ValidarMaiorQueZero(double valor, string nomeCampo)
    {
        if (valor <= 0)
            throw new ArgumentException($"'{nomeCampo}' deve ser maior que zero.", nameof(valor));
    }

    /// <summary>
    /// Valida se um valor está dentro de um intervalo.
    /// </summary>
    /// <param name="valor">Valor a ser validado.</param>
    /// <param name="minimo">Valor mínimo (inclusivo).</param>
    /// <param name="maximo">Valor máximo (inclusivo).</param>
    /// <param name="nomeCampo">Nome do campo para mensagens de erro.</param>
    /// <exception cref="ArgumentException">Lançada quando o valor está fora do intervalo.</exception>
    public static void ValidarIntervalo(double valor, double minimo, double maximo, string nomeCampo)
    {
        if (valor < minimo || valor > maximo)
            throw new ArgumentException($"'{nomeCampo}' deve estar entre {minimo} e {maximo}.", nameof(valor));
    }
}

