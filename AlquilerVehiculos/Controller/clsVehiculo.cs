using AlquilerVehiculos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Controller
{
    internal class clsVehiculo
    {
        public bool Insertar(dtoVehiculo v)
        {
            return v.Insertar(v);
        }

        public bool Actualizar(dtoVehiculo v)
        {
            return v.Actualizar(v);
        }

        public bool Eliminar(int idVehiculo)
        {
            dtoVehiculo ve = new dtoVehiculo();
            return ve.Eliminar(idVehiculo);
        }

        public List<dtoVehiculo> Listar()
        {
            dtoVehiculo ve = new dtoVehiculo();
            return ve.Listar();
        }
    }
}
