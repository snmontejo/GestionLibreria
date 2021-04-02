using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocios.Contexts;
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
    public class AutoresController : Controller
    {
        

        // GET: AutoresController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AutoresController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AutoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoresController/Create
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

        // GET: AutoresController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AutoresController/Edit/5
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

        // GET: AutoresController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AutoresController/Delete/5
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

        // GET: api/Autores?RowCount=5  
        [HttpGet]
        public IEnumerable<Autores> GetAutores()
        {
            try
            {

                IAutorRepository ListAut = new IAutorRepository();
                List<Autores> modelAutores = ListAut.ListarAutores();
                return modelAutores;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]        
        public string CreateAutor(Autores objAutor)
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
        // [ResponseType(typeof(tblCustomer))]
        public string EditaLibro(Autores objAut)
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
        public string Delete(long? id)
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
