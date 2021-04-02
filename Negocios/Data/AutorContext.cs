using Microsoft.EntityFrameworkCore;
using Negocios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Contexts
{
    public class AutorContext:DbContext
    {
        public AutorContext(DbContextOptions<AutorContext> options) : base(options)
        {
                
        }

        public DbSet<Autores> AutorItems { get; set; }

      

       

    }
}
