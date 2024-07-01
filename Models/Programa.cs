using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaU.Models
{
    public class Programa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Aspirante> Aspirantes { get; set; }
    }
}