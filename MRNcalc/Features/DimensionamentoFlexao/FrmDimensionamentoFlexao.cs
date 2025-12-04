using System.ComponentModel;
using System.Globalization;
using MRNcalc.Features.DimensionamentoFlexao;
using MRNcalc.Shared.Helpers;

namespace MRNcalc.Features.DimensionamentoFlexao;

public partial class FrmDimensionamentoFlexao : Form
{
    private readonly DimensionamentoFlexaoService _service;
    private readonly ErrorProvider _errorProvider;

    public FrmDimensionamentoFlexao()
    {
        InitializeComponent();
        _service = new DimensionamentoFlexaoService();
        _errorProvider = new ErrorProvider();
        ConfigurarValidacao();
    }

    private void ConfigurarValidacao()
    {
        // Configurar eventos de validação para todos os campos numéricos
        txtLargura.Validating += (s, e) => ValidarCampoNumerico(txtLargura, "Largura", e);
        txtAltura.Validating += (s, e) => ValidarCampoNumerico(txtAltura, "Altura", e);
        txtMomento.Validating += (s, e) => ValidarCampoNumerico(txtMomento, "Momento característico", e);
        txtDistArmaduras.Validating += (s, e) => ValidarCampoNumerico(txtDistArmaduras, "Distância entre faces e armaduras", e);
        txtGammaF.Validating += (s, e) => ValidarCampoNumerico(txtGammaF, "γf", e);
        txtGammaC.Validating += (s, e) => ValidarCampoNumerico(txtGammaC, "γc", e);
        txtGammaS.Validating += (s, e) => ValidarCampoNumerico(txtGammaS, "γs", e);
        txtKtc.Validating += (s, e) => ValidarCampoNumerico(txtKtc, "ktc", e);
        txtRedist.Validating += (s, e) => ValidarCampoNumerico(txtRedist, "Redistribuição de momentos", e);
        txtLimiteRelativo.Validating += (s, e) => ValidarCampoNumericoOpcional(txtLimiteRelativo, "Limite relativo", e);
    }

    private void ValidarCampoNumerico(Control control, string nomeCampo, CancelEventArgs e)
    {
        string texto = control.Text;
        string? erro = null;

        if (string.IsNullOrWhiteSpace(texto))
        {
            erro = $"Informe um valor numérico para '{nomeCampo}'.";
        }
        else
        {
            try
            {
                double valor = ValidationHelper.ParseDouble(texto, nomeCampo);
                ValidationHelper.ValidarMaiorQueZero(valor, nomeCampo);
            }
            catch (ArgumentException ex)
            {
                erro = ex.Message;
            }
        }

        _errorProvider.SetError(control, erro);
        e.Cancel = false; // Não cancelar, apenas mostrar erro
    }

    private void ValidarCampoNumericoOpcional(Control control, string nomeCampo, CancelEventArgs e)
    {
        string texto = control.Text;
        string? erro = null;

        if (!string.IsNullOrWhiteSpace(texto))
        {
            try
            {
                double valor = ValidationHelper.ParseDouble(texto, nomeCampo);
                ValidationHelper.ValidarIntervalo(valor, 0.0, 0.9, nomeCampo);
            }
            catch (ArgumentException ex)
            {
                erro = ex.Message;
            }
        }

        _errorProvider.SetError(control, erro);
        e.Cancel = false;
    }

    private void btnCalcular_Click(object sender, EventArgs e)
    {
        // Limpar erros anteriores
        _errorProvider.Clear();

        // Validar todos os campos
        if (!ValidarFormulario())
        {
            AtualizarStatus("Corrija os erros antes de calcular.", ToolStripStatusLabelStatus.Error);
            return;
        }

        try
        {
            btnCalcular.Enabled = false;
            AtualizarStatus("Calculando...", ToolStripStatusLabelStatus.Processando);

            // Coletar dados da UI
            var input = ColetarDadosEntrada();

            // Executar cálculo
            var resultado = _service.Calcular(input);

            // Exibir resultados
            ExibirResultados(resultado);

            AtualizarStatus("Cálculo concluído com sucesso.", ToolStripStatusLabelStatus.Sucesso);
        }
        catch (ArgumentException ex)
        {
            _errorProvider.SetError(btnCalcular, ex.Message);
            AtualizarStatus($"Erro de validação: {ex.Message}", ToolStripStatusLabelStatus.Error);
            MessageBox.Show(ex.Message, "Erro de Validação",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (InvalidOperationException ex)
        {
            AtualizarStatus($"Erro no cálculo: {ex.Message}", ToolStripStatusLabelStatus.Error);
            MessageBox.Show(ex.Message, "Erro no Cálculo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            AtualizarStatus($"Erro inesperado: {ex.Message}", ToolStripStatusLabelStatus.Error);
            MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnCalcular.Enabled = true;
        }
    }

    private bool ValidarFormulario()
    {
        bool valido = true;

        // Validar campos obrigatórios
        if (string.IsNullOrWhiteSpace(txtLargura.Text) ||
            string.IsNullOrWhiteSpace(txtAltura.Text) ||
            string.IsNullOrWhiteSpace(txtMomento.Text) ||
            string.IsNullOrWhiteSpace(txtDistArmaduras.Text) ||
            string.IsNullOrWhiteSpace(txtGammaF.Text) ||
            string.IsNullOrWhiteSpace(txtGammaC.Text) ||
            string.IsNullOrWhiteSpace(txtGammaS.Text) ||
            string.IsNullOrWhiteSpace(txtKtc.Text) ||
            string.IsNullOrWhiteSpace(txtRedist.Text))
        {
            valido = false;
        }

        // Validar combos
        if (cmbFck.SelectedItem == null || cmbFyk.SelectedItem == null)
        {
            valido = false;
            if (cmbFck.SelectedItem == null)
                _errorProvider.SetError(cmbFck, "Selecione um valor de fck.");
            if (cmbFyk.SelectedItem == null)
                _errorProvider.SetError(cmbFyk, "Selecione um valor de fy.");
        }

        // Validar limite relativo se não for automático
        if (!rdbLimiteAutomatico.Checked && string.IsNullOrWhiteSpace(txtLimiteRelativo.Text))
        {
            valido = false;
            _errorProvider.SetError(txtLimiteRelativo, "Informe o limite relativo.");
        }

        return valido;
    }

    private DimensionamentoFlexaoInput ColetarDadosEntrada()
    {
        return new DimensionamentoFlexaoInput
        {
            LarguraCm = ValidationHelper.ParseDouble(txtLargura.Text, "Largura"),
            AlturaCm = ValidationHelper.ParseDouble(txtAltura.Text, "Altura"),
            MomentoTfm = ValidationHelper.ParseDouble(txtMomento.Text, "Momento característico"),
            DistFacesArmadurasCm = ValidationHelper.ParseDouble(txtDistArmaduras.Text, "Distância entre faces e armaduras"),
            Fck = ValidationHelper.ParseDouble(cmbFck.SelectedItem!.ToString()!, "fck"),
            Fyk = ValidationHelper.ParseDouble(cmbFyk.SelectedItem!.ToString()!, "fy"),
            GammaF = ValidationHelper.ParseDouble(txtGammaF.Text, "γf"),
            GammaC = ValidationHelper.ParseDouble(txtGammaC.Text, "γc"),
            GammaS = ValidationHelper.ParseDouble(txtGammaS.Text, "γs"),
            Ktc = ValidationHelper.ParseDouble(txtKtc.Text, "ktc"),
            RedistPercent = ValidationHelper.ParseDouble(txtRedist.Text, "Redistribuição de momentos"),
            UsaNBR6118 = rdbNBR6118.Checked,
            LimiteRelativoAutomatico = rdbLimiteAutomatico.Checked,
            LimiteRelativo = rdbLimiteAutomatico.Checked
                ? null
                : ValidationHelper.ParseDouble(txtLimiteRelativo.Text, "Limite relativo")
        };
    }

    private void ExibirResultados(DimensionamentoFlexaoOutput resultado)
    {
        txtAs.Text = resultado.AsCm2.ToString("0.###", CultureInfo.InvariantCulture);
        txtAsLinha.Text = resultado.AsLinhaCm2 > 0
            ? resultado.AsLinhaCm2.ToString("0.###", CultureInfo.InvariantCulture)
            : "0";
        txtProfRelativa.Text = resultado.ProfRelativa.ToString("0.###", CultureInfo.InvariantCulture);
        txtProfLinhaNeutra.Text = resultado.ProfLinhaNeutraCm.ToString("0.###", CultureInfo.InvariantCulture);
    }

    private void rdbLimiteDefinir_CheckedChanged(object sender, EventArgs e)
    {
        txtLimiteRelativo.Enabled = rdbLimiteDefinir.Checked;
        if (rdbLimiteDefinir.Checked)
        {
            txtLimiteRelativo.Focus();
        }
    }

    private void AtualizarStatus(string mensagem, ToolStripStatusLabelStatus status)
    {
        if (statusStrip.Items.Count > 0 && statusStrip.Items[0] is ToolStripStatusLabel lblStatus)
        {
            lblStatus.Text = mensagem;
            lblStatus.ForeColor = status switch
            {
                ToolStripStatusLabelStatus.Sucesso => Color.Green,
                ToolStripStatusLabelStatus.Error => Color.Red,
                ToolStripStatusLabelStatus.Processando => Color.Blue,
                _ => SystemColors.ControlText
            };
        }
    }

    private enum ToolStripStatusLabelStatus
    {
        Normal,
        Sucesso,
        Error,
        Processando
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _errorProvider?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}

