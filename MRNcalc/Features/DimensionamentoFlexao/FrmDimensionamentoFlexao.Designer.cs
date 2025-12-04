using System.Drawing;
using System.Windows.Forms;

namespace MRNcalc.Features.DimensionamentoFlexao;

partial class FrmDimensionamentoFlexao
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    // Controles principais
    private TableLayoutPanel tableLayoutMain;
    private GroupBox grpNorma;
    private RadioButton rdbNBR6118;
    private GroupBox grpSecao;
    private TextBox txtLargura;
    private TextBox txtAltura;
    private GroupBox grpCarregamento;
    private TextBox txtMomento;
    private GroupBox grpMateriais;
    private ComboBox cmbFck;
    private ComboBox cmbFyk;
    private GroupBox grpArmaduras;
    private TextBox txtDistArmaduras;
    private GroupBox grpLimiteLinhaNeutra;
    private RadioButton rdbLimiteAutomatico;
    private RadioButton rdbLimiteDefinir;
    private TextBox txtLimiteRelativo;
    private GroupBox grpPonderadores;
    private TextBox txtGammaF;
    private TextBox txtGammaC;
    private TextBox txtGammaS;
    private TextBox txtKtc;
    private TextBox txtRedist;
    private GroupBox grpResultados;
    private TextBox txtAs;
    private TextBox txtAsLinha;
    private TextBox txtProfRelativa;
    private TextBox txtProfLinhaNeutra;
    private Button btnCalcular;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblStatus;


    #region Windows Form Designer generated code

    /// <summary>
    ///  Método necessário para suporte ao Designer.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        tableLayoutMain = new TableLayoutPanel();
        grpNorma = new GroupBox();
        rdbNBR6118 = new RadioButton();
        grpSecao = new GroupBox();
        txtLargura = new TextBox();
        txtAltura = new TextBox();
        var lblLargura = new Label();
        var lblAltura = new Label();
        grpCarregamento = new GroupBox();
        txtMomento = new TextBox();
        var lblMomento = new Label();
        grpMateriais = new GroupBox();
        cmbFck = new ComboBox();
        cmbFyk = new ComboBox();
        var lblFck = new Label();
        var lblFyk = new Label();
        grpArmaduras = new GroupBox();
        txtDistArmaduras = new TextBox();
        var lblDistArmaduras = new Label();
        grpLimiteLinhaNeutra = new GroupBox();
        rdbLimiteAutomatico = new RadioButton();
        rdbLimiteDefinir = new RadioButton();
        txtLimiteRelativo = new TextBox();
        var lblLimiteValor = new Label();
        grpPonderadores = new GroupBox();
        txtGammaF = new TextBox();
        txtGammaC = new TextBox();
        txtGammaS = new TextBox();
        txtKtc = new TextBox();
        txtRedist = new TextBox();
        var lblGammaF = new Label();
        var lblGammaC = new Label();
        var lblGammaS = new Label();
        var lblKtc = new Label();
        var lblRedist = new Label();
        grpResultados = new GroupBox();
        txtAs = new TextBox();
        txtAsLinha = new TextBox();
        txtProfRelativa = new TextBox();
        txtProfLinhaNeutra = new TextBox();
        var lblAs = new Label();
        var lblAsLinha = new Label();
        var lblProfRelativa = new Label();
        var lblProfLinhaNeutra = new Label();
        btnCalcular = new Button();
        statusStrip = new StatusStrip();
        lblStatus = new ToolStripStatusLabel();
        SuspendLayout();

        // 
        // tableLayoutMain
        // 
        tableLayoutMain.ColumnCount = 2;
        tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutMain.Dock = DockStyle.Fill;
        tableLayoutMain.Location = new Point(0, 0);
        tableLayoutMain.Name = "tableLayoutMain";
        tableLayoutMain.Padding = new Padding(8);
        tableLayoutMain.RowCount = 7;
        tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 85F));
        tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 85F));
        tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
        tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tableLayoutMain.Size = new Size(520, 450);

        // 
        // grpNorma
        // 
        grpNorma.Dock = DockStyle.Fill;
        grpNorma.Padding = new Padding(5);
        grpNorma.Text = "Norma";
        rdbNBR6118.AutoSize = true;
        rdbNBR6118.Location = new Point(10, 18);
        rdbNBR6118.Text = "NBR 6118 / 2023";
        rdbNBR6118.Checked = true;
        grpNorma.Controls.Add(rdbNBR6118);

        // 
        // grpSecao
        // 
        grpSecao.Dock = DockStyle.Fill;
        grpSecao.Padding = new Padding(5);
        grpSecao.Text = "Seção Transversal";
        lblLargura.AutoSize = true;
        lblLargura.Location = new Point(10, 22);
        lblLargura.Text = "Largura (cm)";
        txtLargura.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtLargura.Location = new Point(110, 19);
        txtLargura.Size = new Size(100, 23);
        txtLargura.Text = "20";
        lblAltura.AutoSize = true;
        lblAltura.Location = new Point(10, 50);
        lblAltura.Text = "Altura (cm)";
        txtAltura.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtAltura.Location = new Point(110, 47);
        txtAltura.Size = new Size(100, 23);
        txtAltura.Text = "60";
        grpSecao.Controls.Add(lblLargura);
        grpSecao.Controls.Add(txtLargura);
        grpSecao.Controls.Add(lblAltura);
        grpSecao.Controls.Add(txtAltura);

        // 
        // grpCarregamento
        // 
        grpCarregamento.Dock = DockStyle.Fill;
        grpCarregamento.Padding = new Padding(5);
        grpCarregamento.Text = "Carregamento";
        lblMomento.AutoSize = true;
        lblMomento.Location = new Point(10, 22);
        lblMomento.Text = "Momento Caract. (tf m)";
        txtMomento.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtMomento.Location = new Point(150, 19);
        txtMomento.Size = new Size(60, 23);
        txtMomento.Text = "5";
        grpCarregamento.Controls.Add(lblMomento);
        grpCarregamento.Controls.Add(txtMomento);

        // 
        // grpMateriais
        // 
        grpMateriais.Dock = DockStyle.Fill;
        grpMateriais.Padding = new Padding(5);
        grpMateriais.Text = "Materiais";
        lblFck.AutoSize = true;
        lblFck.Location = new Point(10, 22);
        lblFck.Text = "Concreto fck (MPa)";
        cmbFck.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFck.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmbFck.Location = new Point(130, 19);
        cmbFck.Size = new Size(80, 23);
        cmbFck.Items.AddRange(new object[] { "20", "25", "30", "35", "40", "45", "50", "55", "60", "65", "70", "75", "80", "85", "90" });
        cmbFck.SelectedIndex = 2; // 30
        lblFyk.AutoSize = true;
        lblFyk.Location = new Point(10, 50);
        lblFyk.Text = "Aço fy (MPa)";
        cmbFyk.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFyk.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmbFyk.Location = new Point(130, 47);
        cmbFyk.Size = new Size(80, 23);
        cmbFyk.Items.AddRange(new object[] { "500", "600" });
        cmbFyk.SelectedIndex = 0;
        grpMateriais.Controls.Add(lblFck);
        grpMateriais.Controls.Add(cmbFck);
        grpMateriais.Controls.Add(lblFyk);
        grpMateriais.Controls.Add(cmbFyk);

        // 
        // grpArmaduras
        // 
        grpArmaduras.Dock = DockStyle.Fill;
        grpArmaduras.Padding = new Padding(5);
        grpArmaduras.Text = "Armaduras";
        lblDistArmaduras.AutoSize = true;
        lblDistArmaduras.Location = new Point(10, 22);
        lblDistArmaduras.Text = "Dist. entre faces e armaduras (cm)";
        txtDistArmaduras.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        txtDistArmaduras.Location = new Point(210, 19);
        txtDistArmaduras.Size = new Size(60, 23);
        txtDistArmaduras.Text = "5";
        grpArmaduras.Controls.Add(lblDistArmaduras);
        grpArmaduras.Controls.Add(txtDistArmaduras);

        // 
        // grpLimiteLinhaNeutra
        // 
        grpLimiteLinhaNeutra.Dock = DockStyle.Fill;
        grpLimiteLinhaNeutra.Padding = new Padding(5);
        grpLimiteLinhaNeutra.Text = "Limite Linha Neutra";
        rdbLimiteAutomatico.AutoSize = true;
        rdbLimiteAutomatico.Location = new Point(10, 18);
        rdbLimiteAutomatico.Text = "Automática";
        rdbLimiteAutomatico.Checked = true;
        rdbLimiteDefinir.AutoSize = true;
        rdbLimiteDefinir.Location = new Point(10, 38);
        rdbLimiteDefinir.Text = "Definir";
        rdbLimiteDefinir.CheckedChanged += rdbLimiteDefinir_CheckedChanged;
        lblLimiteValor.AutoSize = true;
        lblLimiteValor.Location = new Point(70, 40);
        lblLimiteValor.Text = "Limite Relativo (x/d)";
        txtLimiteRelativo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        txtLimiteRelativo.Location = new Point(180, 37);
        txtLimiteRelativo.Size = new Size(50, 23);
        txtLimiteRelativo.Text = "0.45";
        txtLimiteRelativo.Enabled = false;
        grpLimiteLinhaNeutra.Controls.Add(rdbLimiteAutomatico);
        grpLimiteLinhaNeutra.Controls.Add(rdbLimiteDefinir);
        grpLimiteLinhaNeutra.Controls.Add(lblLimiteValor);
        grpLimiteLinhaNeutra.Controls.Add(txtLimiteRelativo);

        // 
        // grpPonderadores
        // 
        grpPonderadores.Dock = DockStyle.Fill;
        grpPonderadores.Padding = new Padding(5);
        grpPonderadores.Text = "Ponderadores";
        lblGammaF.AutoSize = true;
        lblGammaF.Location = new Point(10, 22);
        lblGammaF.Text = "Forças (γf)";
        txtGammaF.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtGammaF.Location = new Point(120, 19);
        txtGammaF.Size = new Size(80, 23);
        txtGammaF.Text = "1.4";
        lblGammaC.AutoSize = true;
        lblGammaC.Location = new Point(10, 48);
        lblGammaC.Text = "Concreto (γc)";
        txtGammaC.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtGammaC.Location = new Point(120, 45);
        txtGammaC.Size = new Size(80, 23);
        txtGammaC.Text = "1.4";
        lblGammaS.AutoSize = true;
        lblGammaS.Location = new Point(10, 74);
        lblGammaS.Text = "Aço (γs)";
        txtGammaS.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtGammaS.Location = new Point(120, 71);
        txtGammaS.Size = new Size(80, 23);
        txtGammaS.Text = "1.15";
        lblKtc.AutoSize = true;
        lblKtc.Location = new Point(10, 100);
        lblKtc.Text = "Longa Duração (ktc)";
        txtKtc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtKtc.Location = new Point(120, 97);
        txtKtc.Size = new Size(80, 23);
        txtKtc.Text = "0.85";
        lblRedist.AutoSize = true;
        lblRedist.Location = new Point(10, 126);
        lblRedist.Text = "Redistr. Momentos (%)";
        txtRedist.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtRedist.Location = new Point(120, 123);
        txtRedist.Size = new Size(80, 23);
        txtRedist.Text = "1";
        grpPonderadores.Controls.Add(lblGammaF);
        grpPonderadores.Controls.Add(txtGammaF);
        grpPonderadores.Controls.Add(lblGammaC);
        grpPonderadores.Controls.Add(txtGammaC);
        grpPonderadores.Controls.Add(lblGammaS);
        grpPonderadores.Controls.Add(txtGammaS);
        grpPonderadores.Controls.Add(lblKtc);
        grpPonderadores.Controls.Add(txtKtc);
        grpPonderadores.Controls.Add(lblRedist);
        grpPonderadores.Controls.Add(txtRedist);

        // 
        // grpResultados
        // 
        grpResultados.Dock = DockStyle.Fill;
        grpResultados.Padding = new Padding(5);
        grpResultados.Text = "Resultados";
        lblAs.AutoSize = true;
        lblAs.Location = new Point(10, 22);
        lblAs.Text = "As (cm²)";
        txtAs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtAs.Location = new Point(100, 19);
        txtAs.Size = new Size(100, 23);
        txtAs.ReadOnly = true;
        lblAsLinha.AutoSize = true;
        lblAsLinha.Location = new Point(10, 50);
        lblAsLinha.Text = "As' (cm²)";
        txtAsLinha.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtAsLinha.Location = new Point(100, 47);
        txtAsLinha.Size = new Size(100, 23);
        txtAsLinha.ReadOnly = true;
        lblProfRelativa.AutoSize = true;
        lblProfRelativa.Location = new Point(10, 78);
        lblProfRelativa.Text = "Prof. Relativa (x/d)";
        txtProfRelativa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtProfRelativa.Location = new Point(130, 75);
        txtProfRelativa.Size = new Size(70, 23);
        txtProfRelativa.ReadOnly = true;
        lblProfLinhaNeutra.AutoSize = true;
        lblProfLinhaNeutra.Location = new Point(10, 106);
        lblProfLinhaNeutra.Text = "Prof. linha neutra (cm)";
        txtProfLinhaNeutra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtProfLinhaNeutra.Location = new Point(150, 103);
        txtProfLinhaNeutra.Size = new Size(50, 23);
        txtProfLinhaNeutra.ReadOnly = true;
        grpResultados.Controls.Add(lblAs);
        grpResultados.Controls.Add(txtAs);
        grpResultados.Controls.Add(lblAsLinha);
        grpResultados.Controls.Add(txtAsLinha);
        grpResultados.Controls.Add(lblProfRelativa);
        grpResultados.Controls.Add(txtProfRelativa);
        grpResultados.Controls.Add(lblProfLinhaNeutra);
        grpResultados.Controls.Add(txtProfLinhaNeutra);

        // 
        // btnCalcular
        // 
        btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnCalcular.Dock = DockStyle.Fill;
        btnCalcular.Text = "Calcular";
        btnCalcular.UseVisualStyleBackColor = true;
        btnCalcular.Click += btnCalcular_Click;

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
        statusStrip.Location = new Point(0, 450);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(520, 22);
        lblStatus.Name = "lblStatus";
        lblStatus.Text = "Pronto";
        lblStatus.Spring = true;

        // Adicionar controles ao TableLayoutPanel
        tableLayoutMain.Controls.Add(grpNorma, 0, 0);
        tableLayoutMain.Controls.Add(grpSecao, 0, 1);
        tableLayoutMain.Controls.Add(grpCarregamento, 0, 2);
        tableLayoutMain.Controls.Add(grpMateriais, 0, 3);
        tableLayoutMain.Controls.Add(grpArmaduras, 0, 4);
        tableLayoutMain.Controls.Add(grpLimiteLinhaNeutra, 0, 5);
        tableLayoutMain.Controls.Add(grpPonderadores, 1, 0);
        tableLayoutMain.SetRowSpan(grpPonderadores, 3);
        tableLayoutMain.Controls.Add(grpResultados, 1, 3);
        tableLayoutMain.SetRowSpan(grpResultados, 2);
        tableLayoutMain.Controls.Add(btnCalcular, 0, 6);
        tableLayoutMain.SetColumnSpan(btnCalcular, 2);

        // 
        // FrmDimensionamentoFlexao
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(520, 472);
        Controls.Add(tableLayoutMain);
        Controls.Add(statusStrip);
        FormBorderStyle = FormBorderStyle.Sizable;
        MinimumSize = new Size(520, 472);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Dimensionamento a Flexão Simples - v2.0";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

