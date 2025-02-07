using AlquilerVehiculos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Controller
{
    internal class clsCliente
    {// Insertar
        public bool Insertar(dtoCliente cliente)
        {
            return cliente.Insertar(cliente);
        }

        // Actualizar
        public bool Actualizar(dtoCliente cliente)
        {
            return cliente.Actualizar(cliente);
        }

        // Eliminar
        public bool Eliminar(int idCliente)
        {
            dtoCliente cli = new dtoCliente();
            return cli.Eliminar(idCliente);
        }

        // Listar
        public List<dtoCliente> Listar()
        {
            dtoCliente cli = new dtoCliente();
            return cli.Listar();
        }
    }
}
