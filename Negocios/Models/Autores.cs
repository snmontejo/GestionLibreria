using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Models
{
    public class Autores
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int Id_Ciudad { get; set; }
        public string Sexo { get; set; }

    }
}
