using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Models
{
    public class Libros
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public DateTime FechaEscritura { get; set; }
        public double Costo { get; set; }
        public Autores Autor { get; set; }
    }
}
