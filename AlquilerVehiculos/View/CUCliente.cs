using AlquilerVehiculos.Controller;
using AlquilerVehiculos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlquilerVehiculos.View
{
    public partial class CUCliente : UserControl
    {
        private int _clienteSeleccionadoId = 0;
        public CUCliente()
        {
            InitializeComponent();
        }
        private void LimpiarControles()
        {
            _clienteSeleccionadoId = 0;
            txtNombre.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            btnGuardar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        private void CUCliente_Load(object sender, EventArgs e)
        {
            CargarClientesEnGrid();
            LimpiarControles();
        }
        private void CargarClientesEnGrid()
        {
            clsCliente clienteCtrl = new clsCliente();
            var lista = clienteCtrl.Listar(); 

            dgvContenedor.DataSource = lista;

            
        }

        private void dgvContenedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvContenedor.Rows[e.RowIndex];

                _clienteSeleccionadoId = Convert.ToInt32(fila.Cells["IDCliente"].Value);
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();

                btnGuardar.Enabled = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                dtoCliente c = new dtoCliente
                {
                    Nombre = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim()
                };

                clsCliente clienteCtrl = new clsCliente();
                bool resultado = clienteCtrl.Insertar(c);

                if (resultado)
                {
                    MessageBox.Show("Cliente guardado correctamente");
                    CargarClientesEnGrid();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                dtoCliente c = new dtoCliente
                {
                    IDCliente = _clienteSeleccionadoId,
                    Nombre = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim()
                };

                clsCliente clienteCtrl = new clsCliente();
                bool resultado = clienteCtrl.Actualizar(c);

                if (resultado)
                {
                    MessageBox.Show("Cliente actualizado correctamente");
                    CargarClientesEnGrid();
                    LimpiarControles(); // Vuelve al estado inicial
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_clienteSeleccionadoId <= 0)
                {
                    MessageBox.Show("Debe seleccionar un cliente primero.");
                    return;
                }

                // Confirmar la eliminación
                DialogResult dr = MessageBox.Show("¿Desea eliminar este cliente?",
                                                  "Confirmación",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    clsCliente clienteCtrl = new clsCliente();
                    bool resultado = clienteCtrl.Eliminar(_clienteSeleccionadoId);

                    if (resultado)
                    {
                        MessageBox.Show("Cliente eliminado correctamente");
                        CargarClientesEnGrid();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el cliente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}");
            }
        }
    }
}
