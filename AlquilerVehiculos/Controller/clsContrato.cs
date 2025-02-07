using AlquilerVehiculos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Controller
{
    internal class clsContrato
    {
        public bool Insertar(dtoContrato c)
        {
            return c.Insertar(c);
        }

        public bool Actualizar(dtoContrato c)
        {
            return c.Actualizar(c);
        }

        public bool Eliminar(int idContrato)
        {
            dtoContrato cont = new dtoContrato();
            return cont.Eliminar(idContrato);
        }

        public List<dtoContrato> Listar()
        {
            dtoContrato cont = new dtoContrato();
            return cont.Listar();
        }
    }
}
