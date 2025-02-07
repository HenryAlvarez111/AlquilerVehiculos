using AlquilerVehiculos.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Model
{
    internal class dtoContrato
    { // Propiedades que representan las columnas de la tabla "Contrato"
        public int IDContrato { get; set; }
        public int IDCliente { get; set; }
        public int IDVehiculo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Insertar
        public bool Insertar(dtoContrato c)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spCreateContrato", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDCliente", c.IDCliente);
                        cmd.Parameters.AddWithValue("@IDVehiculo", c.IDVehiculo);
                        cmd.Parameters.AddWithValue("@FechaInicio", c.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", c.FechaFin);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Insertar contrato: {ex.Message}");
            }
            return resultado;
        }

        // Actualizar
        public bool Actualizar(dtoContrato c)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateContrato", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDContrato", c.IDContrato);
                        cmd.Parameters.AddWithValue("@IDCliente", c.IDCliente);
                        cmd.Parameters.AddWithValue("@IDVehiculo", c.IDVehiculo);
                        cmd.Parameters.AddWithValue("@FechaInicio", c.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", c.FechaFin);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Actualizar contrato: {ex.Message}");
            }
            return resultado;
        }

        // Eliminar
        public bool Eliminar(int idContrato)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spDeleteContrato", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDContrato", idContrato);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Eliminar contrato: {ex.Message}");
            }
            return resultado;
        }

        // Listar
        public List<dtoContrato> Listar()
        {
            List<dtoContrato> lista = new List<dtoContrato>();
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spReadContrato", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDContrato", DBNull.Value);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dtoContrato cn = new dtoContrato
                                {
                                    IDContrato = Convert.ToInt32(dr["IDContrato"]),
                                    IDCliente = Convert.ToInt32(dr["IDCliente"]),
                                    IDVehiculo = Convert.ToInt32(dr["IDVehiculo"]),
                                    FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                    FechaFin = Convert.ToDateTime(dr["FechaFin"])
                                };
                                lista.Add(cn);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Listar contratos: {ex.Message}");
            }
            return lista;
        }
    }
}
