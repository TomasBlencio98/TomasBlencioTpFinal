using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TomasBlencioTpFinal.BL;


namespace TomasBlencioTpFinal.Windows
{
    public partial class FrmAE : Form
    {
        public FrmAE()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        TrianguloEquilatero trianguloEquilatero;
        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (trianguloEquilatero==null)
                {
                    trianguloEquilatero = new TrianguloEquilatero(); 
                }
                trianguloEquilatero.Lado = double.Parse(LadoTextBox.Text);
                trianguloEquilatero.Borde = BordeComboBox.SelectedItem.ToString();
                DialogResult = DialogResult.OK; 
            }
        }

        private bool ValidarDatos()
        {
            var valido = true;
            if (!double.TryParse(LadoTextBox.Text,out var valor))
            {
                valido = false;
                errorProvider1.SetError(LadoTextBox, "Valor del lado mal ingresado");
                LadoTextBox.Clear();
                LadoTextBox.Focus();             
            }
            else if (valor<=0)
            {
                valido = false;
                errorProvider1.SetError(LadoTextBox, "El valor del lado debe ser positivo");
                LadoTextBox.Clear();
                LadoTextBox.Focus();
            }
            else
            {
                valido = true;
            }

            return valido;
        }

        internal TrianguloEquilatero GetTrianguloEquilatero()
        {
            return trianguloEquilatero;
        }

        internal void SetTrianguloEquilatero(TrianguloEquilatero trianguloEquilatero)
        {
            this.trianguloEquilatero = trianguloEquilatero;
        }

        protected override void OnLoad(EventArgs e)
        {
            BordeComboBox.SelectedIndex = 0;
            if (trianguloEquilatero!=null)
            {
                LadoTextBox.Text = trianguloEquilatero.Lado.ToString();
                BordeComboBox.Text = trianguloEquilatero.Borde.ToString();
            }
        }

    }
}
