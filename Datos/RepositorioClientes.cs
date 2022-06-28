using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Datos
  
{
    public class RepositorioClientes : ConexionBD, DatosDAO.ICRUD<Cliente>
    {
        public string Actualizar(Cliente obj)
        {
            try
            {
                string _sql = string.Format("UPDATE [dbo].[Clientes] SET [Nombres] = '{0}' ,[TipoCliente] ='{1}' WHERE [IdCliente] = '{2}'", obj.Nombre, obj.TipoCliente, obj.IDC);

                var cmd = new SqlCommand(_sql, conexion);
                AbrirConnexion();
                int filas = cmd.ExecuteNonQuery();
                CerrarConnexion();
                if (filas == 1)
                {
                    return "se Actualizo el registro del cliente cuyo id = :" + obj.IDC;
                }
                return "Imposible actualizar el registro del cliente cuyo id = :" + obj.IDC;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        public Cliente BuscarID(string id)
        {
            try
            {
                string _sql = string.Format("select * from clientes where IdCliente='{0}'", id);
                var cmd = new SqlCommand(_sql, conexion);
                AbrirConnexion();
                var reader = cmd.ExecuteReader();
                reader.Read();
                var cliente = new Entidades.Cliente(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                CerrarConnexion();
                return cliente;
            }
            catch (Exception)
            {

                return null;
            }

            
           

        }

        public string Eliminar(Cliente obj)
        {
            try
            {
                    string _sql=string.Format("DELETE FROM [dbo].[Clientes] WHERE IdCliente='{0}'",obj.IDC);

                    var cmd = new SqlCommand(_sql, conexion);
                    AbrirConnexion();
                    int filas = cmd.ExecuteNonQuery();
                    CerrarConnexion();
                    if (filas == 1)
                    {
                        return "se elimino el registro del cliente cuyo id = :" + obj.IDC;
                    }
                    return "Imposible se eliminar el registro del cliente cuyo id = :" + obj.IDC;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            

        }

        public string Insertar(Cliente obj)
        {
            try
            {
                string _sql = string.Format("INSERT INTO[dbo].[Clientes] VALUES('"+obj.IDC+"','"+obj.Nombre+"','"+obj.TipoCliente+"')");
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

        public List<Cliente> Todos(string condicion)
        {
            string _sql = string.Format("select * from clientes where IdCliente like '{0}%' or Nombres like '{1}%'",condicion,condicion);
            System.Data.DataTable tabla = new DataTable("clientes");
            SqlDataAdapter adapter = new SqlDataAdapter(_sql, conexion);

            adapter.Fill(tabla);

            List<Cliente> lista = new List<Cliente>();

            foreach (var fila in tabla.Rows)
            {
                
                lista.Add(map((DataRow)fila));
            }
            return lista;
        }

        Cliente map(DataRow fila)
        {
            Cliente cliente = new Cliente();
            cliente.IDC = (string)fila[0];
            cliente.Nombre = (string)fila[1];
            cliente.TipoCliente = (string)fila[2];
            return cliente;
        }
    }
}