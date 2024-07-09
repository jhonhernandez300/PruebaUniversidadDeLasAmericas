using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaU.Models
{
    public class AspirantesPeriodosAcademicos
    {
        [Key, Column(Order = 0)]
        public int AspiranteId { get; set; }

        [Key, Column(Order = 1)]
        public Aspirante Aspirante { get; set; }

        public int PeriodoAcademicoId { get; set; }
        public PeriodoAcademico PeriodoAcademico { get; set; }
    }
}