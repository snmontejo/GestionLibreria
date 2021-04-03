using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Negocios.Data
{
    public class Ciudades
    {
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public Nacionalidades Nacionalidad { get; set; }
    }
}
