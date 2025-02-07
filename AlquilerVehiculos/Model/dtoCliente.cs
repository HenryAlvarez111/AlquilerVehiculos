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
    internal class dtoCliente
    {// Propiedades que representan las columnas de la tabla "Cliente"
        public int IDCliente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        // ========== INSERTAR ==========
        public bool Insertar(dtoCliente cliente)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spCreateCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Insertar cliente: {ex.Message}");
            }
            return resultado;
        }

        // ========== ACTUALIZAR ==========
        public bool Actualizar(dtoCliente cliente)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDCliente", cliente.IDCliente);
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Actualizar cliente: {ex.Message}");
            }
            return resultado;
        }

        // ========== ELIMINAR ==========
        public bool Eliminar(int idCliente)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spDeleteCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDCliente", idCliente);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Eliminar cliente: {ex.Message}");
            }
            return resultado;
        }

        // ========== LISTAR ==========
        public List<dtoCliente> Listar()
        {
            List<dtoCliente> lista = new List<dtoCliente>();
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spReadCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Si tu SP usa lógica para cuando @IDCliente es NULL, puedes usar:
                        cmd.Parameters.AddWithValue("@IDCliente", DBNull.Value);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dtoCliente cli = new dtoCliente
                                {
                                    IDCliente = Convert.ToInt32(dr["IDCliente"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Telefono = dr["Telefono"].ToString()
                                };
                                lista.Add(cli);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Listar clientes: {ex.Message}");
            }
            return lista;
        }
    }
}
