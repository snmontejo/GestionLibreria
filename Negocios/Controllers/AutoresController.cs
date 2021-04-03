using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocios.Data;
using Negocios.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Negocios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {


        // GET: /Autores/ListarAutores  
        [HttpGet]
        [Route("ListarAutores")]
        public string  ListarAutores()
        {
            try
            {

                IAutorRepository ListAut = new IAutorRepository();
                DataTable modelAutores = ListAut.ListarAutores();
                // return modelAutores;
               // return modelAutores;

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(modelAutores);
                return JSONString;


            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("CreateAutor")]
        public string CreateAutor([FromBody] Autores objAutor)
        {
            try
            {
                IAutorRepository InsAut = new IAutorRepository();
                Int32 message = 0;

                if ((objAutor.Nombre != null) && (objAutor.Id_Ciudad != 0) && (objAutor.Sexo  != null))
                {
                    message = InsAut.InsertAutores(objAutor);
                }
                else { message = -1; }
                return message.ToString();
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("EditarAutor")]
        public string EditarAutor(Autores objAut)
        {
            try
            {
                IAutorRepository InsAut = new IAutorRepository();
                Int32 message = 0;
                message = InsAut.Actualiza_Autores(objAut);
                return message.ToString();

            }
            catch
            {
                throw;
            }

        }

        [HttpDelete]
        [Route("BorrarAutor")]
        public string BorrarAutor(long? id)
        {
            try
            {
                IAutorRepository Del = new IAutorRepository();
                Int32 message = 0;
                message = Del.DeleteAutores(id);
                return message.ToString();
            }
            catch
            {
                throw;
            }
        }
    }
}
