using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TomasBlencioTpFinal.BL;
using TomasBlencioTpFinal.DL;

namespace TomasBlencioTpFinal.Windows
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private RepositorioTriangulosEquilatero repositorio;
        private List<TrianguloEquilatero> lista;

        private void SalirToolStripButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            repositorio = new RepositorioTriangulosEquilatero();
            lista = repositorio.GetLista();
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            DatosDataGridView.Rows.Clear();
            foreach (var trianguloEquilatero in lista)
            {
                DataGridViewRow r = ConstruirFila();
                RellenarFila(r, trianguloEquilatero);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            DatosDataGridView.Rows.Add(r);
        }

        private void RellenarFila(DataGridViewRow r, TrianguloEquilatero trianguloEquilatero)
        {
            r.Cells[ColumnaLado.Index].Value = trianguloEquilatero.Lado;
            r.Cells[ColumnaBorde.Index].Value = trianguloEquilatero.Borde;
            r.Cells[ColumnaPerimetro.Index].Value = trianguloEquilatero.GetPerimetro();
            r.Cells[ColumnaSuperficie.Index].Value = trianguloEquilatero.GetSuperficie();

            r.Tag = trianguloEquilatero;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(DatosDataGridView);
            return r;
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            FrmAE frm = new FrmAE();
            frm.Text = "Agregar nuevo";
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.OK)
            {
                TrianguloEquilatero trianguloEquilatero = frm.GetTrianguloEquilatero();
                repositorio.Agregar(trianguloEquilatero);
                DataGridViewRow r = ConstruirFila();
                RellenarFila(r, trianguloEquilatero);
                AgregarFila(r);
            }

        }

        private void BorrarToolStripButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow r = DatosDataGridView.SelectedRows[0];
            TrianguloEquilatero trianguloEquilatero = r.Tag as TrianguloEquilatero;
            DialogResult dr = MessageBox.Show($"¿Estas seguro que desea eliminar el triangulo" +
                $"equilatero de valor de lado {trianguloEquilatero.Lado} y borde {trianguloEquilatero.Borde}?","Question",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (dr==DialogResult.Yes)
            {
                DatosDataGridView.Rows.Remove(r);
                repositorio.Borrar(trianguloEquilatero);


                MessageBox.Show("Triangulo equilatero borrado exitosamente", "Information",
                   MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void EditarToolStripButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow r = DatosDataGridView.SelectedRows[0];
            TrianguloEquilatero trianguloEquilatero = r.Tag as TrianguloEquilatero;
            TrianguloEquilatero trianguloEquilateroCopia = trianguloEquilatero.Clone() as TrianguloEquilatero;
            FrmAE frm = new FrmAE();
            frm.SetTrianguloEquilatero(trianguloEquilatero);
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.OK)
            {
                trianguloEquilatero = frm.GetTrianguloEquilatero();
                repositorio.Editar(trianguloEquilatero, trianguloEquilateroCopia);
                RellenarFila(r, trianguloEquilatero);
                MessageBox.Show("Triangulo equilatero editado correctamente", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ActualizarToolStripButton_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetLista();
            MostrarDatosEnGrilla();
        }

        private void OrdenarAscendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio.OrdenarAscendente();
            MostrarDatosEnGrilla();
        }

        private void OrdenarDescendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio.OrdenarDescendente();
            MostrarDatosEnGrilla();
        }
    }
}
