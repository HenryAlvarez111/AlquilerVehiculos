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
    public partial class CUContrato : UserControl
    {
        private int _contratoSeleccionadoId = 0;
        public CUContrato()
        {
            InitializeComponent();
        }
        private void LimpiarControles()
        {
            _contratoSeleccionadoId = 0;

            // Regresar al primer elemento de los ComboBox (o ninguno)
            if (cmbCliente.Items.Count > 0)
                cmbCliente.SelectedIndex = 0;
            if (cmbVehiculo.Items.Count > 0)
                cmbVehiculo.SelectedIndex = 0;

            // Fecha actual u otra por defecto
            dtpFInicio.Value = DateTime.Today;
            dtpFFin.Value = DateTime.Today;

            btnGuardar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        private void CUContrato_Load(object sender, EventArgs e)
        {
            CargarClientesEnCombo();
            CargarVehiculosEnCombo();
            CargarContratosEnGrid();
            LimpiarControles();

        }
        private void CargarClientesEnCombo()
        {
            clsCliente clienteCtrl = new clsCliente();
            var listaClientes = clienteCtrl.Listar(); 

            
            cmbCliente.DataSource = listaClientes;
            cmbCliente.DisplayMember = "Nombre";     // Qué campo mostrar
            cmbCliente.ValueMember = "IDCliente";    // Qué campo usar como valor
        }
        private void CargarVehiculosEnCombo()
        {
            clsVehiculo vehiculoCtrl = new clsVehiculo();
            var listaVehiculos = vehiculoCtrl.Listar(); 

            cmbVehiculo.DataSource = listaVehiculos;
            cmbVehiculo.DisplayMember = "Marca";       
            cmbVehiculo.ValueMember = "IDVehiculo";
        }
        private void CargarContratosEnGrid()
        {
            clsContrato contratoCtrl = new clsContrato();
            var listaContratos = contratoCtrl.Listar(); // List<dtoContrato>

            dgvContenedor.DataSource = listaContratos;

            
        }

        private void dgvContenedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvContenedor.Rows[e.RowIndex];

                _contratoSeleccionadoId = Convert.ToInt32(fila.Cells["IDContrato"].Value);

                // Cargar en los comboBox y dateTimePickers
                cmbCliente.SelectedValue = Convert.ToInt32(fila.Cells["IDCliente"].Value);
                cmbVehiculo.SelectedValue = Convert.ToInt32(fila.Cells["IDVehiculo"].Value);

                dtpFInicio.Value = Convert.ToDateTime(fila.Cells["FechaInicio"].Value);
                dtpFFin.Value = Convert.ToDateTime(fila.Cells["FechaFin"].Value);

                // Ajustar botones
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                dtoContrato contrato = new dtoContrato
                {
                    // Tomamos los valores del combo
                    IDCliente = Convert.ToInt32(cmbCliente.SelectedValue),
                    IDVehiculo = Convert.ToInt32(cmbVehiculo.SelectedValue),
                    FechaInicio = dtpFInicio.Value,
                    FechaFin = dtpFFin.Value
                };

                clsContrato contratoCtrl = new clsContrato();
                bool resultado = contratoCtrl.Insertar(contrato);

                if (resultado)
                {
                    MessageBox.Show("Contrato guardado con éxito.");
                    CargarContratosEnGrid();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el contrato.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar contrato: {ex.Message}");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                dtoContrato contrato = new dtoContrato
                {
                    IDContrato = _contratoSeleccionadoId,
                    IDCliente = Convert.ToInt32(cmbCliente.SelectedValue),
                    IDVehiculo = Convert.ToInt32(cmbVehiculo.SelectedValue),
                    FechaInicio = dtpFInicio.Value,
                    FechaFin = dtpFFin.Value
                };

                clsContrato contratoCtrl = new clsContrato();
                bool resultado = contratoCtrl.Actualizar(contrato);

                if (resultado)
                {
                    MessageBox.Show("Contrato actualizado correctamente.");
                    CargarContratosEnGrid();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el contrato.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar contrato: {ex.Message}");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_contratoSeleccionadoId <= 0)
                {
                    MessageBox.Show("No se ha seleccionado ningún contrato.");
                    return;
                }

                // Confirmar la eliminación
                DialogResult dr = MessageBox.Show("¿Desea eliminar este contrato?",
                                                  "Confirmación",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    clsContrato contratoCtrl = new clsContrato();
                    bool resultado = contratoCtrl.Eliminar(_contratoSeleccionadoId);

                    if (resultado)
                    {
                        MessageBox.Show("Contrato eliminado correctamente.");
                        CargarContratosEnGrid();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el contrato.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar contrato: {ex.Message}");
            }
        }
    }
}
