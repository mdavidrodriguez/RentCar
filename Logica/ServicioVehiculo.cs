using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
namespace Logica
{
    public class ServicioVehiculo
    {
        RepositorioVehiculos repositorio = new RepositorioVehiculos();
        List<Vehiculo> listavehiculos;

        public string  Actualizar(Vehiculo vehiculo)
        {
            return repositorio.Actualizar(vehiculo);

        }
        public string Guardar(Vehiculo vehiculo)
        {

            return repositorio.Insertar(vehiculo);

        }
        public List<Vehiculo> Consultar()
        {
            return listavehiculos;
        }
        public Vehiculo BuscarAutos(string placa)
        {
            foreach (var item in listavehiculos)
            {
                if (item.Cliente.IDC == placa)
                {
                    return item;
                }

            }
            return null;
        }

        public string Eliminar(String Placa)
        {
            Vehiculo vehiculo = BuscarAutos(Placa);

            if (vehiculo == null)
            {

                return "";

            }
            else
            {
                listavehiculos.Remove(vehiculo);

                return "";

            }
        }

        public Vehiculo BuscarR(String Placa)
        {
            foreach (var item in listavehiculos)
            {
                if (item.PlacaVehiculo == Placa)
                {
                    return item;
                }

            }
            return null;

        }
    }

       
    }

