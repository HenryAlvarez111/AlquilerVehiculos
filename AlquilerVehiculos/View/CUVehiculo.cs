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
    public partial class CUVehiculo : UserControl
    {
        private int _vehiculoSeleccionadoId = 0;
        public CUVehiculo()
        {
            InitializeComponent();
        }
        private void LimpiarControles()
        {
            _vehiculoSeleccionadoId = 0;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;

            btnGuardar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void CUVehiculo_Load(object sender, EventArgs e)
        {
            CargarVehiculosEnGrid();
            LimpiarControles();
        }
        private void CargarVehiculosEnGrid()
        {
           
            clsVehiculo vehiculoCtrl = new clsVehiculo();
            var lista = vehiculoCtrl.Listar(); 

            dgvContenedor.DataSource = lista;

            
        }

        private void dgvContenedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex >= 0)
            {
               
                DataGridViewRow fila = dgvContenedor.Rows[e.RowIndex];

                _vehiculoSeleccionadoId = Convert.ToInt32(fila.Cells["IDVehiculo"].Value);
                txtMarca.Text = fila.Cells["Marca"].Value.ToString();
                txtModelo.Text = fila.Cells["Modelo"].Value.ToString();

               
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
               
                dtoVehiculo v = new dtoVehiculo
                {
                    Marca = txtMarca.Text.Trim(),
                    Modelo = txtModelo.Text.Trim()
                };

                clsVehiculo vehiculoCtrl = new clsVehiculo();
                bool resultado = vehiculoCtrl.Insertar(v);

                if (resultado)
                {
                    MessageBox.Show("Vehículo guardado correctamente");
                    CargarVehiculosEnGrid();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el vehículo");
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
               
                dtoVehiculo v = new dtoVehiculo
                {
                    IDVehiculo = _vehiculoSeleccionadoId,
                    Marca = txtMarca.Text.Trim(),
                    Modelo = txtModelo.Text.Trim()
                };

                clsVehiculo vehiculoCtrl = new clsVehiculo();
                bool resultado = vehiculoCtrl.Actualizar(v);

                if (resultado)
                {
                    MessageBox.Show("Vehículo actualizado correctamente");
                    CargarVehiculosEnGrid();
                    LimpiarControles(); 
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el vehículo");
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
                if (_vehiculoSeleccionadoId <= 0)
                {
                    MessageBox.Show("Debe seleccionar un vehículo primero.");
                    return;
                }

                // Se puede preguntar confirmación
                DialogResult dr = MessageBox.Show("¿Seguro desea eliminar este vehículo?",
                                                  "Confirmar",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    clsVehiculo vehiculoCtrl = new clsVehiculo();
                    bool resultado = vehiculoCtrl.Eliminar(_vehiculoSeleccionadoId);

                    if (resultado)
                    {
                        MessageBox.Show("Vehículo eliminado correctamente");
                        CargarVehiculosEnGrid();
                        LimpiarControles(); 
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el vehículo");
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
