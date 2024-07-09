using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaU.Models
{
    public class TodosDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El primer nombre es requerido.")]
        public string PrimerNombre { get; set; }
                
        [Required(ErrorMessage = "El primer apellido es requerido.")]
        public string PrimerApellido { get; set; }

        public int TelefonoFijo { get; set; }

        public int Celular { get; set; }

        public string Correo { get; set; }

        [Required(ErrorMessage = "El tipo de documento es requerido.")]
        public string TipoDeDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de documento debe ser un número entero.")]
        public string NumeroDeDocumento { get; set; }        

        [Required(ErrorMessage = "El estado es requerido.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El ID del programa es requerido.")]
        public string ProgramaNombre { get; set; }

        [Required(ErrorMessage = "El ID de la sede es requerido.")]
        public string SedeNombre { get; set; }

        [Required(ErrorMessage = "El ID de la sede es requerido.")]
        public string PeriodoAcademico { get; set; }        
    }
}