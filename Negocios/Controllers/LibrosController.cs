using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocios.Data;
using Negocios.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : ControllerBase
    {
        
       

        [HttpGet]
        [Route("ListarLibros")]
        public string ListarLibros()
        {
            try
            {

                ILibroRepository ListLibr = new ILibroRepository();
                DataTable modelLibros = ListLibr.ListarLibros();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(modelLibros);
                return JSONString;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("CreateLibro")]
        public string CreateLibro(Libros objLibr)
        {
            try
            {
                ILibroRepository InsLibr = new ILibroRepository();
                Int32 message = 0;

                if ((objLibr.Nombre != null) && (objLibr.FechaEscritura != null) && (objLibr.Costo != null) && (objLibr.Id_Autor != null))
                {
                    message = InsLibr.InsertLibros(objLibr);
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
        [Route("EditarLibro")]
        public string EditarLibro(Libros objLib)
        {
            try
            {
                ILibroRepository InsLibr = new ILibroRepository();
                Int32 message = 0;
                message = InsLibr.UpdateLibros(objLib);
                return message.ToString();

            }
            catch
            {
                throw;
            }

        }

        [HttpDelete]
        [Route("BorrarLibro")]
        public string BorrarLibro(long? id)
        {
            try
            {
                ILibroRepository Del = new ILibroRepository();
                Int32 message = 0;
                message = Del.DeleteLibros(id);
                return message.ToString();
            }
            catch
            {
                throw;
            }
        }
    }


}
