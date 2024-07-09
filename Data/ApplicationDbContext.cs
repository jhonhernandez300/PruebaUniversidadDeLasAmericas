using PruebaU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace PruebaU.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Aspirante> Aspirantes { get; set; }
        public DbSet<PeriodoAcademico> PeriodosAcademicos { get; set; }
        public DbSet<Programa> Programas { get; set; }
        public DbSet<Sede> Sedes { get; set; }
        public DbSet<AspirantesPeriodosAcademicos> AspirantesPeriodosAcademicos { get; set; }
        public DbSet<AspiranteDto> AspiranteDto { get; set; }

        public ApplicationDbContext() : base("DefaultConnection") { }
        
    }
}