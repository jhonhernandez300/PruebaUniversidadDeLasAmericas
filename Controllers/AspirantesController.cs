using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaU.Models;
using PruebaU.Data;
using System.Data.SqlClient;

namespace PruebaU.Controllers
{
    public class AspirantesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create()
        {
            //ViewBag.Programas = new SelectList(db.Programas, "Id", "Nombre");
            ViewBag.Sedes = new SelectList(db.Sedes, "Id", "Nombre");
            
            var periodosAcademicos = db.Database.SqlQuery<PeriodoAcademico>("ObtenerTodosLosPeriodosAcademicos").ToList();
            ViewBag.PeriodosAcademicos = new SelectList(periodosAcademicos, "Id", "Periodo");

            var programas = db.Database.SqlQuery<Programa>("ObtenerTodosLosProgramas").ToList();
            ViewBag.Programas = new SelectList(programas, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aspirante aspirante, int[] PeriodosAcademicos)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ApplicationDbContext())
                {
                    var sql = @"
                EXEC GuardarAspirante 
                    @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, 
                    @FechaDeNacimiento, @Departamento, @Pais, @Ciudad, 
                    @GrupoSanguineo, @TipoDeDocumento, @NumeroDeDocumento, 
                    @FechaDeExpedicion, @PaisDeExpedicion, @DepartamentoDeExpedicion, 
                    @CiudadDeExpedicion, @Sexo, @EstadoCivil, @Estado, @Tipo, @ProgramaId, @SedeId";

                    context.Database.ExecuteSqlCommand(
                        sql,
                        new SqlParameter("@PrimerNombre", aspirante.PrimerNombre),
                        new SqlParameter("@SegundoNombre", aspirante.SegundoNombre),
                        new SqlParameter("@PrimerApellido", aspirante.PrimerApellido),
                        new SqlParameter("@SegundoApellido", aspirante.SegundoApellido),
                        new SqlParameter("@FechaDeNacimiento", aspirante.FechaDeNacimiento),
                        new SqlParameter("@Departamento", aspirante.Departamento),
                        new SqlParameter("@Pais", aspirante.Pais),
                        new SqlParameter("@Ciudad", aspirante.Ciudad),
                        new SqlParameter("@GrupoSanguineo", aspirante.GrupoSanguineo),
                        new SqlParameter("@TipoDeDocumento", aspirante.TipoDeDocumento),
                        new SqlParameter("@NumeroDeDocumento", aspirante.NumeroDeDocumento),
                        new SqlParameter("@FechaDeExpedicion", aspirante.FechaDeExpedicion),
                        new SqlParameter("@PaisDeExpedicion", aspirante.PaisDeExpedicion),
                        new SqlParameter("@DepartamentoDeExpedicion", aspirante.DepartamentoDeExpedicion),
                        new SqlParameter("@CiudadDeExpedicion", aspirante.CiudadDeExpedicion),
                        new SqlParameter("@Sexo", aspirante.Sexo),
                        new SqlParameter("@EstadoCivil", aspirante.EstadoCivil),
                        new SqlParameter("@Estado", aspirante.Estado),
                        new SqlParameter("@Tipo", aspirante.Tipo),
                        new SqlParameter("@ProgramaId", aspirante.ProgramaId),
                        new SqlParameter("@SedeId", aspirante.SedeId)
                    );

                    foreach (var periodoId in PeriodosAcademicos)
                    {
                        context.Database.ExecuteSqlCommand(
                            "INSERT INTO AspirantePeriodoAcademico (AspiranteId, PeriodoAcademicoId) VALUES (@AspiranteId, @PeriodoAcademicoId)",
                            new SqlParameter("@AspiranteId", aspirante.Id),
                            new SqlParameter("@PeriodoAcademicoId", periodoId)
                        );
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.Programas = new SelectList(db.Programas, "Id", "Nombre", aspirante.ProgramaId);
            ViewBag.Sedes = new SelectList(db.Sedes, "Id", "Nombre", aspirante.SedeId);
            ViewBag.PeriodosAcademicos = new SelectList(db.PeriodosAcademicos, "Id", "Periodo");
            return View(aspirante);
        }

    }
}