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
    public class IAutorRepository
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

        public List<Autores> ListarAutores()
        {
            Conn.Open();
        

            try
            {
                List<Autores> _listAutores = new List<Autores>();

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("List_Autores", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                //objCommand.Parameters.AddWithValue("@PageNo", PageNo);
                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    Autores aut = new Autores();
                    aut.Id = Convert.ToInt32(_Reader["Id"]);
                    aut.Nombre = _Reader["Nombre"].ToString();
                    aut.Id_Ciudad = Convert.ToInt32(_Reader["Id_Ciudad"]);
                    aut.Sexo = _Reader["Sexo"].ToString();
                    _listAutores.Add(aut);
                }

                return _listAutores;
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

        public Int32 InsertAutores(Autores objAutor)
        {
            
            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("CreateAutor", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Nombre", objAutor.Nombre);
                objCommand.Parameters.AddWithValue("@Id_Ciudad", objAutor.Id_Ciudad);
                objCommand.Parameters.AddWithValue("@Sexo", objAutor.Sexo);

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

        public Int32 Actualiza_Autores(Autores objAutor)
        {

            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("UpdateAutor", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Id", objAutor.Id);
                objCommand.Parameters.AddWithValue("@Nombre", objAutor.Nombre);
                objCommand.Parameters.AddWithValue("@Id_Ciudad", objAutor.Id_Ciudad);
                objCommand.Parameters.AddWithValue("@Sexo", objAutor.Sexo);

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

        public Int32 DeleteAutores(long? id)
        {

            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("DeleteAutores", Conn);
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
