using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoteAutos2017.Controladores;
using LoteAutos2017.Modelo;

namespace LoteAutos2017
{
    public partial class frmVentaAuto : Form
    {
        private Cliente nCliente = null;
        private Auto nAuto = null;

        public void MostrarDatosCliente(int idCliente)
        {
            if (idCliente > 0)
            {
                nCliente = ClienteManager.BuscarPorID(idCliente);
                txtNombre.Text = nCliente.sNombre;
                txtApellidos.Text = nCliente.sApellidos;
                txtTelefono.Text = nCliente.sTelefono;
                txtEmail.Text = nCliente.sEmail;
                DesactivarControles(true);
            }
            else
            {
                DesactivarControles(false);
                nCliente = null;
            }
        }

        private void DesactivarControles(bool b)
        {
            foreach (object obj in this.groupBox1.Controls)
            {
                if (obj is TextBox)
                {
                    TextBox txt = (TextBox) obj;
                    txt.ReadOnly = b;
                    if (!b)
                    {
                        txt.Text = "";
                    }
                }
            }
        }

        public frmVentaAuto()
        {
            InitializeComponent();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (!txtNombre.ReadOnly)
            {

                frmBuscarCliente vHija = new frmBuscarCliente(this);
                vHija.ShowDialog();
            }
            else
            {
                MostrarDatosCliente(0);
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox4.Text == "")
            {
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.nAuto=AutoManager.BuscarPorId(Convert.ToInt32(textBox4.Text));  
                    txtMarca.Text = nAuto.sMarca;
                    txtModelo.Text = nAuto.sModelo;
                    txtAnio.Text = nAuto.iAnio.ToString();
                    txtNoSerie.Text = nAuto.sNumeroSerie;
                    txtDescripcion.Text = nAuto.sDescripcion;
                    textBox1.Text = nAuto.PrecioVenta.ToString("c");
                    textBox2.Text = (nAuto.PrecioVenta*0.16).ToString("c");


                }
            }
        }
    }
}
