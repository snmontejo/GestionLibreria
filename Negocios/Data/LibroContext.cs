using Microsoft.EntityFrameworkCore;
using Negocios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Contexts
{
    public class LibroContext:DbContext
    {
        public LibroContext(DbContextOptions<LibroContext> options) : base(options)
        {

        }

        public DbSet<Libros>LibroItems { get; set; }






    }
}
