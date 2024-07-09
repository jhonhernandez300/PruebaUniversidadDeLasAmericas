using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaU.Models
{
    public class AspiranteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El primer nombre es requerido.")]
        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es requerido.")]
        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public int TelefonoFijo { get; set; }

        public int Celular { get; set; }

        public string Correo { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        [DataType(DataType.Date)]
        public DateTime FechaDeNacimiento { get; set; }

        [Required(ErrorMessage = "El departamento es requerido.")]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "El país es requerido.")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "La ciudad es requerida.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El grupo sanguíneo es requerido.")]
        public string GrupoSanguineo { get; set; }

        [Required(ErrorMessage = "El tipo de documento es requerido.")]
        public string TipoDeDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de documento debe ser un número entero.")]
        public string NumeroDeDocumento { get; set; }

        [Required(ErrorMessage = "La fecha de expedición es requerida.")]
        [DataType(DataType.Date)]
        public DateTime FechaDeExpedicion { get; set; }

        [Required(ErrorMessage = "El país de expedición es requerido.")]
        public string PaisDeExpedicion { get; set; }

        [Required(ErrorMessage = "El departamento de expedición es requerido.")]
        public string DepartamentoDeExpedicion { get; set; }

        [Required(ErrorMessage = "La ciudad de expedición es requerida.")]
        public string CiudadDeExpedicion { get; set; }

        [Required(ErrorMessage = "El sexo es requerido.")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "El estado civil es requerido.")]
        public string EstadoCivil { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El tipo de aspirante es requerido.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El ID del programa es requerido.")]
        public int ProgramaId { get; set; }

        [Required(ErrorMessage = "El ID de la sede es requerido.")]
        public int SedeId { get; set; }

        [Required(ErrorMessage = "El ID de la sede es requerido.")]
        public int PeriodoAcademicoId { get; set; }

        public virtual Programa Programa { get; set; }
        public virtual Sede Sede { get; set; }
        public virtual PeriodoAcademico PeriodoAcademico { get; set; }
    }
}