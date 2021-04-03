using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Negocios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Data
{
    public class ILibroRepository
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

        public DataTable ListarLibros()
        {
            Conn.Open();


            try
            {
                DataTable DtLibros = new DataTable();

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("List_Libros", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(objCommand);
                da.Fill(DtLibros);

                return DtLibros;
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

        public Int32 InsertLibros(Libros objLibr)
        {

            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("CreateLibros", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Nombre", objLibr.Nombre);
                objCommand.Parameters.AddWithValue("@FechaEscritura", objLibr.FechaEscritura);
                objCommand.Parameters.AddWithValue("@Costo", objLibr.Costo);
                objCommand.Parameters.AddWithValue("@Id_Autor", objLibr.Id_Autor);

                result = Convert.ToInt32(objCommand.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
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

        public Int32 UpdateLibros(Libros objLibr)
        {

            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("UpdateLibros", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Id", objLibr.Id);
                objCommand.Parameters.AddWithValue("@Nombre", objLibr.Nombre);
                objCommand.Parameters.AddWithValue("@FechaEscritura", objLibr.FechaEscritura);
                objCommand.Parameters.AddWithValue("@Costo", objLibr.Costo);
                objCommand.Parameters.AddWithValue("@Id_Autor", objLibr.Id_Autor);

                result = Convert.ToInt32(objCommand.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
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

        public Int32 DeleteLibros(long? id)
        {
            
            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("DeleteLibros", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Id", id);
                result = Convert.ToInt32(objCommand.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
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
