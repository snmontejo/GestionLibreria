using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocios.Data;
using Negocios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : Controller
    {
        // GET: LibrosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LibrosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LibrosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibrosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibrosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LibrosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibrosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LibrosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IEnumerable<Libros> GetAutores()
        {
            try
            {

                ILibroRepository ListLibr = new ILibroRepository();
                List<Libros> modelLibros = ListLibr.ListarLibros();
                return modelLibros;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public string CreateLibros(Libros objLibr)
        {
            try
            {
                ILibroRepository InsLibr = new ILibroRepository();
                Int32 message = 0;

                if ((objLibr.Nombre != null) && (objLibr.FechaEscritura != null) && (objLibr.Costo != null) && (objLibr.Autor != null))
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
        // [ResponseType(typeof(tblCustomer))]
        public string EditaLibro(Libros objLib)
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
        public string Delete(long? id)
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
