using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatosDAO;
using Entidades;
using System.Data.SqlClient;
using System.Data;
namespace Datos
{
    public class RepositorioVehiculos : ConexionBD, ICRUDVehiculo<Vehiculo>
    {
        public string Actualizar(Vehiculo objs)
        {
            try
            {
                string _sql = string.Format("UPDATE [dbo].[Vehiculos] SET [Marca] = '{0}' ,[KilometrajeActual] ='{1}' WHERE [Placa] = '{2}'", objs.Marca, objs.KilometrajeActual, objs.PlacaVehiculo);

                var cmd = new SqlCommand(_sql, conexion);
                AbrirConnexion();
                int filas = cmd.ExecuteNonQuery();
                CerrarConnexion();
                if (filas == 1)
                {
                    return "se Actualizo el registro del vehiculo con placa = :" + objs.PlacaVehiculo;
                }
                return "Imposible actualizar el registro del vehiculo con placa = :" + objs.PlacaVehiculo;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }


        public Vehiculo BuscarPlaca(string placa)
        {
            try
            {
                string _sql = string.Format("select * from Vehiculos where Placa='{0}'", placa);
                var cmd = new SqlCommand(_sql, conexion);
                AbrirConnexion();
                var reader = cmd.ExecuteReader();
                reader.Read();
                var vehiculo = new Entidades.Vehiculo(reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
                CerrarConnexion();
                return vehiculo;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public string Eliminar(Vehiculo objs)
        {
            try
            {
                string _sql = string.Format("DELETE FROM [dbo].[Vehiculos] WHERE Placa='{0}'", objs.PlacaVehiculo);

                var cmd = new SqlCommand(_sql, conexion);
                AbrirConnexion();
                int filas = cmd.ExecuteNonQuery();
                CerrarConnexion();
                if (filas == 1)
                {
                    return "se elimino el registro del vehiculo con placa = :" + objs.PlacaVehiculo;
                }
                return "Imposible se eliminar el registro del vehiculo con placa= :" + objs.PlacaVehiculo;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Insertar(Vehiculo objs)
        {
            try
            {
                string _sql = string.Format("INSERT INTO[dbo].[Vehiculos] VALUES('" + objs.PlacaVehiculo + "','" + objs.Marca + "','" + objs.KilometrajeActual + "')");
                var cmd = new SqlCommand(_sql, conexion);
                AbrirConnexion();
                int filas = cmd.ExecuteNonQuery();
                CerrarConnexion();
                if (filas > 0)
                {
                    return "Datos guardados satisfactoriamente";
                }
                return "No se pudo guardar los datos";

            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public List<Vehiculo> Todos(string condicion)
        {
            string _sql = string.Format("select * from Vehiculos where Placa like '{0}%' or Marca like '{1}%'", condicion, condicion);
            DataTable tabla = new DataTable("vehiculos");
            SqlDataAdapter adapter = new SqlDataAdapter(_sql, conexion);

            adapter.Fill(tabla);

            List<Vehiculo> lista = new List<Vehiculo>();

            foreach (var fila in tabla.Rows)
            {

                lista.Add(map((DataRow)fila));
            }
            return lista;
        }

        Vehiculo map(DataRow fila)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.PlacaVehiculo = (string)fila[0];
            vehiculo.Marca = (string)fila[1];
            vehiculo.KilometrajeActual =(int)fila[2];
            return vehiculo;
        }
    }
}
