using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Data
{
    public class ICiudadRepository
    {
        private static string GetDbConnection()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            string strConnection = builder.Build().GetConnectionString("Connection");

            return strConnection;
        }
        SqlConnection Conn = new SqlConnection(GetDbConnection());

        public DataTable ListarCiudadXNacionalidad(int id_Nacionalidad)
        {
            Conn.Open();


            try
            {
                DataTable dataTable = new DataTable();
                string query = "select * from Ciudades where  id_Nacionalidad ="+id_Nacionalidad;
                SqlCommand cmd = new SqlCommand(query, Conn);
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);

                return dataTable;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }

        public Ciudades ObtenerCiudadXId(int Id)
        {
            Conn.Open();


            try
            {
                
                string query = "select * from Ciudades where  id =" + Id;
                SqlCommand cmd = new SqlCommand(query, Conn);
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader _Reader = cmd.ExecuteReader();
                Ciudades ciudad = null;
                while (_Reader.Read())
                {
                    ciudad = new Ciudades();
                    ciudad.Id= Convert.ToInt32(_Reader["Id"].ToString());
                    ciudad.Descripcion= _Reader["Descripcion"].ToString();
                    INacionalidadRepository na = new INacionalidadRepository();
                    Nacionalidades nac = na.ObtenerNacionalidadXId(Convert.ToInt32(_Reader["id_Nacionalidad"].ToString()));
                    ciudad.Nacionalidad = nac;
                }

                    return ciudad;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }


    }
}
