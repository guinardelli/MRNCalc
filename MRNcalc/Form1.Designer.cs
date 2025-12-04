using System.Drawing;
using System.Windows.Forms;

namespace MRNcalc;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    // Controles principais (necessários em outros arquivos)
    private GroupBox grpNorma;
    private RadioButton rdbNBR6118;
    private RadioButton rdbEurocode;

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
    internal TextBox txtLimiteRelativo;

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

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Método necessário para suporte ao Designer.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        grpNorma = new GroupBox();
        rdbEurocode = new RadioButton();
        rdbNBR6118 = new RadioButton();
        grpSecao = new GroupBox();
        txtAltura = new TextBox();
        txtLargura = new TextBox();
        var lblAltura = new Label();
        var lblLargura = new Label();
        grpCarregamento = new GroupBox();
        txtMomento = new TextBox();
        var lblMomento = new Label();
        grpMateriais = new GroupBox();
        cmbFyk = new ComboBox();
        cmbFck = new ComboBox();
        var lblFyk = new Label();
        var lblFck = new Label();
        grpArmaduras = new GroupBox();
        txtDistArmaduras = new TextBox();
        var lblDistArmaduras = new Label();
        grpLimiteLinhaNeutra = new GroupBox();
        txtLimiteRelativo = new TextBox();
        rdbLimiteDefinir = new RadioButton();
        rdbLimiteAutomatico = new RadioButton();
        var lblLimiteValor = new Label();
        grpPonderadores = new GroupBox();
        txtRedist = new TextBox();
        txtKtc = new TextBox();
        txtGammaS = new TextBox();
        txtGammaC = new TextBox();
        txtGammaF = new TextBox();
        var lblRedist = new Label();
        var lblKtc = new Label();
        var lblGammaS = new Label();
        var lblGammaC = new Label();
        var lblGammaF = new Label();
        grpResultados = new GroupBox();
        txtProfLinhaNeutra = new TextBox();
        txtProfRelativa = new TextBox();
        txtAsLinha = new TextBox();
        txtAs = new TextBox();
        var lblProfLinhaNeutra = new Label();
        var lblProfRelativa = new Label();
        var lblAsLinha = new Label();
        var lblAs = new Label();
        btnCalcular = new Button();
        SuspendLayout();

        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(495, 510);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Dimensionamento a Flexão Simples - v1.1";

        // 
        // grpNorma
        // 
        grpNorma.Text = "Norma";
        grpNorma.Location = new Point(12, 12);
        grpNorma.Size = new Size(210, 70);

        // rdbNBR6118
        rdbNBR6118.AutoSize = true;
        rdbNBR6118.Location = new Point(10, 22);
        rdbNBR6118.Text = "NBR 6118 / 2023";
        rdbNBR6118.Checked = true;

        // rdbEurocode
        rdbEurocode.AutoSize = true;
        rdbEurocode.Location = new Point(10, 43);
        rdbEurocode.Text = "Eurocode 2 / 2023";

        grpNorma.Controls.Add(rdbNBR6118);
        grpNorma.Controls.Add(rdbEurocode);

        // 
        // grpSecao
        // 
        grpSecao.Text = "Seção Transversal";
        grpSecao.Location = new Point(12, 88);
        grpSecao.Size = new Size(210, 90);

        lblLargura.AutoSize = true;
        lblLargura.Location = new Point(10, 25);
        lblLargura.Text = "Largura (cm)";

        txtLargura.Location = new Point(110, 22);
        txtLargura.Size = new Size(80, 23);
        txtLargura.Text = "20";

        lblAltura.AutoSize = true;
        lblAltura.Location = new Point(10, 55);
        lblAltura.Text = "Altura (cm)";

        txtAltura.Location = new Point(110, 52);
        txtAltura.Size = new Size(80, 23);
        txtAltura.Text = "60";

        grpSecao.Controls.Add(lblLargura);
        grpSecao.Controls.Add(txtLargura);
        grpSecao.Controls.Add(lblAltura);
        grpSecao.Controls.Add(txtAltura);

        // 
        // grpCarregamento
        // 
        grpCarregamento.Text = "Carregamento";
        grpCarregamento.Location = new Point(12, 184);
        grpCarregamento.Size = new Size(210, 60);

        lblMomento.AutoSize = true;
        lblMomento.Location = new Point(10, 26);
        lblMomento.Text = "Momento Caract. (tf m)";

        txtMomento.Location = new Point(150, 22);
        txtMomento.Size = new Size(40, 23);
        txtMomento.Text = "5";

        grpCarregamento.Controls.Add(lblMomento);
        grpCarregamento.Controls.Add(txtMomento);

        // 
        // grpMateriais
        // 
        grpMateriais.Text = "Materiais";
        grpMateriais.Location = new Point(12, 250);
        grpMateriais.Size = new Size(210, 90);

        lblFck.AutoSize = true;
        lblFck.Location = new Point(10, 25);
        lblFck.Text = "Concreto fck (MPa)";

        cmbFck.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFck.Location = new Point(130, 22);
        cmbFck.Size = new Size(60, 23);
        cmbFck.Items.AddRange(new object[] { "20", "25", "30", "35", "40" });
        cmbFck.SelectedIndex = 2; // 30

        lblFyk.AutoSize = true;
        lblFyk.Location = new Point(10, 55);
        lblFyk.Text = "Aço fy (MPa)";

        cmbFyk.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFyk.Location = new Point(130, 52);
        cmbFyk.Size = new Size(60, 23);
        cmbFyk.Items.AddRange(new object[] { "500", "600" });
        cmbFyk.SelectedIndex = 0;

        grpMateriais.Controls.Add(lblFck);
        grpMateriais.Controls.Add(cmbFck);
        grpMateriais.Controls.Add(lblFyk);
        grpMateriais.Controls.Add(cmbFyk);

        // 
        // grpArmaduras
        // 
        grpArmaduras.Text = "Armaduras";
        grpArmaduras.Location = new Point(12, 346);
        grpArmaduras.Size = new Size(210, 60);

        lblDistArmaduras.AutoSize = true;
        lblDistArmaduras.Location = new Point(10, 26);
        lblDistArmaduras.Text = "Dist. entre faces e armaduras (cm)";

        txtDistArmaduras.Location = new Point(180, 22);
        txtDistArmaduras.Size = new Size(20, 23);
        txtDistArmaduras.Text = "5";

        grpArmaduras.Controls.Add(lblDistArmaduras);
        grpArmaduras.Controls.Add(txtDistArmaduras);

        // 
        // grpLimiteLinhaNeutra
        // 
        grpLimiteLinhaNeutra.Text = "Limite Linha Neutra";
        grpLimiteLinhaNeutra.Location = new Point(12, 412);
        grpLimiteLinhaNeutra.Size = new Size(210, 80);

        rdbLimiteAutomatico.AutoSize = true;
        rdbLimiteAutomatico.Location = new Point(10, 22);
        rdbLimiteAutomatico.Text = "Automática";
        rdbLimiteAutomatico.Checked = true;

        rdbLimiteDefinir.AutoSize = true;
        rdbLimiteDefinir.Location = new Point(10, 45);
        rdbLimiteDefinir.Text = "Definir";
        rdbLimiteDefinir.CheckedChanged += rdbLimiteDefinir_CheckedChanged;

        lblLimiteValor.AutoSize = true;
        lblLimiteValor.Location = new Point(80, 47);
        lblLimiteValor.Text = "Limite Relativo (x/d)";

        txtLimiteRelativo.Location = new Point(170, 44);
        txtLimiteRelativo.Size = new Size(30, 23);
        txtLimiteRelativo.Text = "0.45";
        txtLimiteRelativo.Enabled = false;

        grpLimiteLinhaNeutra.Controls.Add(rdbLimiteAutomatico);
        grpLimiteLinhaNeutra.Controls.Add(rdbLimiteDefinir);
        grpLimiteLinhaNeutra.Controls.Add(lblLimiteValor);
        grpLimiteLinhaNeutra.Controls.Add(txtLimiteRelativo);

        // 
        // grpPonderadores
        // 
        grpPonderadores.Text = "Ponderadores";
        grpPonderadores.Location = new Point(235, 12);
        grpPonderadores.Size = new Size(240, 160);

        lblGammaF.AutoSize = true;
        lblGammaF.Location = new Point(10, 25);
        lblGammaF.Text = "Forças (γf)";
        txtGammaF.Location = new Point(150, 22);
        txtGammaF.Size = new Size(60, 23);
        txtGammaF.Text = "1.4";

        lblGammaC.AutoSize = true;
        lblGammaC.Location = new Point(10, 50);
        lblGammaC.Text = "Concreto (γc)";
        txtGammaC.Location = new Point(150, 47);
        txtGammaC.Size = new Size(60, 23);
        txtGammaC.Text = "1.4";

        lblGammaS.AutoSize = true;
        lblGammaS.Location = new Point(10, 75);
        lblGammaS.Text = "Aço (γs)";
        txtGammaS.Location = new Point(150, 72);
        txtGammaS.Size = new Size(60, 23);
        txtGammaS.Text = "1.15";

        lblKtc.AutoSize = true;
        lblKtc.Location = new Point(10, 100);
        lblKtc.Text = "Longa Duração (ktc)";
        txtKtc.Location = new Point(150, 97);
        txtKtc.Size = new Size(60, 23);
        txtKtc.Text = "0.85";

        lblRedist.AutoSize = true;
        lblRedist.Location = new Point(10, 125);
        lblRedist.Text = "Redistr. Momentos (%)";
        txtRedist.Location = new Point(150, 122);
        txtRedist.Size = new Size(60, 23);
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
        grpResultados.Text = "Resultados";
        grpResultados.Location = new Point(235, 178);
        grpResultados.Size = new Size(240, 200);

        lblAs.AutoSize = true;
        lblAs.Location = new Point(10, 25);
        lblAs.Text = "As (cm²)";

        txtAs.Location = new Point(120, 22);
        txtAs.Size = new Size(100, 23);
        txtAs.ReadOnly = true;

        lblAsLinha.AutoSize = true;
        lblAsLinha.Location = new Point(10, 60);
        lblAsLinha.Text = "As' (cm²)";

        txtAsLinha.Location = new Point(120, 57);
        txtAsLinha.Size = new Size(100, 23);
        txtAsLinha.ReadOnly = true;

        lblProfRelativa.AutoSize = true;
        lblProfRelativa.Location = new Point(10, 95);
        lblProfRelativa.Text = "Prof. Relativa (x/d)";

        txtProfRelativa.Location = new Point(150, 92);
        txtProfRelativa.Size = new Size(70, 23);
        txtProfRelativa.ReadOnly = true;

        lblProfLinhaNeutra.AutoSize = true;
        lblProfLinhaNeutra.Location = new Point(10, 130);
        lblProfLinhaNeutra.Text = "Prof. da linha neutra (cm)";

        txtProfLinhaNeutra.Location = new Point(170, 127);
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
        btnCalcular.Text = "Calcular";
        btnCalcular.Location = new Point(235, 396);
        btnCalcular.Size = new Size(240, 40);
        btnCalcular.Click += btnCalcular_Click;

        // 
        // Adiciona controles ao formulário
        // 
        Controls.Add(grpNorma);
        Controls.Add(grpSecao);
        Controls.Add(grpCarregamento);
        Controls.Add(grpMateriais);
        Controls.Add(grpArmaduras);
        Controls.Add(grpLimiteLinhaNeutra);
        Controls.Add(grpPonderadores);
        Controls.Add(grpResultados);
        Controls.Add(btnCalcular);

        ResumeLayout(false);
    }

    #endregion
}
