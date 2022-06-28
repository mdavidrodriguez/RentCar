using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Vehiculo
    {

        public string PlacaVehiculo { get; set; }
        public string Marca { get; set; }
        public int KilometrajeActual { get; set; }
        public Cliente  Cliente { get; set; }
        public Vehiculo()
        {

        }

        public Vehiculo(string placaVehiculo, string marca, int kilometrajeActual)
        {
            PlacaVehiculo = placaVehiculo;
            Marca = marca;
            KilometrajeActual = kilometrajeActual;
        }
        public  string linea()
        {
            return PlacaVehiculo + ";" + Marca + ";" + KilometrajeActual;
        }
    }
}
