using AlquilerVehiculos.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContrato_Click(object sender, EventArgs e)
        {
            CUContrato frmPrueba = new CUContrato();
            pnlContenido.Controls.Clear();
            frmPrueba.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(frmPrueba);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            CUCliente frmPrueba = new CUCliente();
            pnlContenido.Controls.Clear();
            frmPrueba.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(frmPrueba);
        }

        private void btnVehiculos_Click(object sender, EventArgs e)
        {
            CUVehiculo frmPrueba = new CUVehiculo();
            pnlContenido.Controls.Clear();
            frmPrueba.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(frmPrueba);
        }
    }
}
