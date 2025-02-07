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
    internal class dtoVehiculo
    { // Propiedades que representan las columnas de la tabla "Vehiculo"
        public int IDVehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        // Insertar
        public bool Insertar(dtoVehiculo v)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spCreateVehiculo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Marca", v.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", v.Modelo);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Insertar vehiculo: {ex.Message}");
            }
            return resultado;
        }

        // Actualizar
        public bool Actualizar(dtoVehiculo v)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateVehiculo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDVehiculo", v.IDVehiculo);
                        cmd.Parameters.AddWithValue("@Marca", v.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", v.Modelo);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Actualizar vehiculo: {ex.Message}");
            }
            return resultado;
        }

        // Eliminar
        public bool Eliminar(int idVehiculo)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spDeleteVehiculo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDVehiculo", idVehiculo);

                        cmd.ExecuteNonQuery();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Eliminar vehiculo: {ex.Message}");
            }
            return resultado;
        }

        // Listar
        public List<dtoVehiculo> Listar()
        {
            List<dtoVehiculo> lista = new List<dtoVehiculo>();
            try
            {
                using (SqlConnection con = Conexion.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spReadVehiculo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDVehiculo", DBNull.Value);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dtoVehiculo ve = new dtoVehiculo
                                {
                                    IDVehiculo = Convert.ToInt32(dr["IDVehiculo"]),
                                    Marca = dr["Marca"].ToString(),
                                    Modelo = dr["Modelo"].ToString()
                                };
                                lista.Add(ve);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Listar vehiculos: {ex.Message}");
            }
            return lista;
        }
    }
}
