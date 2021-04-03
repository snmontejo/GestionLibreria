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
    public class INacionalidadRepository
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

        public DataTable ListarNacionalidades()
        {
            Conn.Open();
        

            try
            {
                DataTable dataTable = new DataTable();
                string query = "select * from Nacionalidades ";
                SqlCommand cmd = new SqlCommand(query, Conn);
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                
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
        public Nacionalidades ObtenerNacionalidadXId(int Id)
        {
            Conn.Open();


            try
            {

                string query = "select * from Nacionalidades where  id =" + Id;
                SqlCommand cmd = new SqlCommand(query, Conn);
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader _Reader = cmd.ExecuteReader();
                Nacionalidades nacion=null;
                while (_Reader.Read())
                {
                    nacion = new Nacionalidades();
                    nacion.Id = Convert.ToInt32(_Reader["Id"].ToString());
                    nacion.Descripcion = _Reader["Descripcion"].ToString();

                    
                }

                return nacion;
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
