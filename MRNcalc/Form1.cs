using System;
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
        // Aqui futuramente entra a lógica de dimensionamento.
        MessageBox.Show("Cálculo ainda não implementado. A interface já está configurada.", "Informação",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void rdbLimiteDefinir_CheckedChanged(object sender, EventArgs e)
    {
        txtLimiteRelativo.Enabled = rdbLimiteDefinir.Checked;
    }
}
