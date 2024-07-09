using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaU.Models;
using PruebaU.Data;
using System.Data.SqlClient;
using System.Drawing;
using Rotativa;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace PruebaU.Controllers
{
    public class AspirantesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Table()
        {
            var todosDto = db.Database.SqlQuery<TodosDto>("ObtenerTodosLosAspirantes").ToList();
            return View(todosDto);
        }        

    public ActionResult ExportToExcel()
    {
        var todosDto = db.Database.SqlQuery<TodosDto>("ObtenerTodosLosAspirantes").ToList();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (ExcelPackage excelPackage = new ExcelPackage())
        {
            // Set some properties of the Excel document
            excelPackage.Workbook.Properties.Author = "TuNombre";
            excelPackage.Workbook.Properties.Title = "Lista de Aspirantes";

            // Create a sheet
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Aspirantes");

            // Add the headers
            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "Primer Nombre";
            worksheet.Cells[1, 3].Value = "Primer Apellido";
            worksheet.Cells[1, 4].Value = "Estado";
            worksheet.Cells[1, 5].Value = "Sede";
            worksheet.Cells[1, 6].Value = "Programa";
            worksheet.Cells[1, 7].Value = "Período";
            worksheet.Cells[1, 8].Value = "TipoDeDocumento";
            worksheet.Cells[1, 9].Value = "NumeroDeDocumento";
            worksheet.Cells[1, 10].Value = "Período Académico";
            worksheet.Cells[1, 11].Value = "Teléfono Fijo";
            worksheet.Cells[1, 12].Value = "Celular";
            worksheet.Cells[1, 13].Value = "Correo";

                // Add values
                int row = 2;
            foreach (var item in todosDto)
            {
                worksheet.Cells[row, 1].Value = item.Id;
                worksheet.Cells[row, 2].Value = item.PrimerNombre;
                worksheet.Cells[row, 3].Value = item.PrimerApellido;
                worksheet.Cells[row, 4].Value = item.Estado;
                worksheet.Cells[row, 5].Value = item.SedeNombre;
                worksheet.Cells[row, 6].Value = item.ProgramaNombre;
                worksheet.Cells[row, 7].Value = item.PeriodoAcademico;
                worksheet.Cells[row, 8].Value = item.TipoDeDocumento;
                worksheet.Cells[row, 9].Value = item.NumeroDeDocumento;
                worksheet.Cells[row, 10].Value = item.PeriodoAcademico;
                worksheet.Cells[row, 11].Value = item.TelefonoFijo;
                worksheet.Cells[row, 12].Value = item.Celular;
                worksheet.Cells[row, 13].Value = item.Correo;
                    row++;
            }

            // Format the header
            using (ExcelRange range = worksheet.Cells[1, 1, 1, 10])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                range.Style.Font.Color.SetColor(Color.White);
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Save your file
            var excelData = excelPackage.GetAsByteArray();

            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Aspirantes.xlsx");
        }
    }


    public ActionResult ExportPdf()
        {
            var todosDto = db.Database.SqlQuery<TodosDto>("ObtenerTodosLosAspirantes").ToList();
            return new ViewAsPdf("Table", todosDto)
            {
                FileName = "Aspirantes.pdf"
            };
        }

        public ActionResult Create()
        {            
            ViewBag.Sedes = new SelectList(db.Sedes, "Id", "Nombre");            

            var programas = db.Database.SqlQuery<Programa>("ObtenerTodosLosProgramas").ToList();
            ViewBag.Programas = new SelectList(programas, "Id", "Nombre");

            var periodosAcademicos = db.Database.SqlQuery<PeriodoAcademico>("ObtenerTodosLosPeriodosAcademicos").ToList();
            ViewBag.PeriodosAcademicos = new SelectList(periodosAcademicos, "Id", "Periodo");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AspiranteDto aspiranteDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var sql = @"
                EXEC GuardarAspirante 
                    @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido,                     
                    @FechaDeNacimiento, @Departamento, @Pais, @Ciudad, 
                    @GrupoSanguineo, @TipoDeDocumento, @NumeroDeDocumento, 
                    @FechaDeExpedicion, @PaisDeExpedicion, @DepartamentoDeExpedicion, 
                    @CiudadDeExpedicion, @Sexo, @EstadoCivil, @Estado, @Tipo, @ProgramaId, @SedeId, @PeriodoAcademicoId,
                    @TelefonoFijo, @Celular, @Correo";

                        context.Database.ExecuteSqlCommand(
                            sql,
                            new SqlParameter("@PrimerNombre", aspiranteDto.PrimerNombre),
                            new SqlParameter("@SegundoNombre", aspiranteDto.SegundoNombre),
                            new SqlParameter("@PrimerApellido", aspiranteDto.PrimerApellido),
                            new SqlParameter("@SegundoApellido", aspiranteDto.SegundoApellido),                            
                            new SqlParameter("@FechaDeNacimiento", aspiranteDto.FechaDeNacimiento),
                            new SqlParameter("@Departamento", aspiranteDto.Departamento),
                            new SqlParameter("@Pais", aspiranteDto.Pais),
                            new SqlParameter("@Ciudad", aspiranteDto.Ciudad),
                            new SqlParameter("@GrupoSanguineo", aspiranteDto.GrupoSanguineo),
                            new SqlParameter("@TipoDeDocumento", aspiranteDto.TipoDeDocumento),
                            new SqlParameter("@NumeroDeDocumento", aspiranteDto.NumeroDeDocumento),
                            new SqlParameter("@FechaDeExpedicion", aspiranteDto.FechaDeExpedicion),
                            new SqlParameter("@PaisDeExpedicion", aspiranteDto.PaisDeExpedicion),
                            new SqlParameter("@DepartamentoDeExpedicion", aspiranteDto.DepartamentoDeExpedicion),
                            new SqlParameter("@CiudadDeExpedicion", aspiranteDto.CiudadDeExpedicion),
                            new SqlParameter("@Sexo", aspiranteDto.Sexo),
                            new SqlParameter("@EstadoCivil", aspiranteDto.EstadoCivil),
                            new SqlParameter("@Estado", aspiranteDto.Estado),
                            new SqlParameter("@Tipo", aspiranteDto.Tipo),
                            new SqlParameter("@ProgramaId", aspiranteDto.ProgramaId),
                            new SqlParameter("@SedeId", aspiranteDto.SedeId),
                            new SqlParameter("@PeriodoAcademicoId", aspiranteDto.PeriodoAcademicoId),
                            new SqlParameter("@TelefonoFijo", aspiranteDto.TelefonoFijo),
                            new SqlParameter("@Celular", aspiranteDto.Celular),
                            new SqlParameter("@Correo", aspiranteDto.Correo)
                        );
                    }
                    TempData["SuccessMessage"] = "El aspirante se ha guardado correctamente.";
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al guardar el aspirante: " + ex.Message);
                }
            }

            // Si hay errores de validación, recargar la vista con los datos
            ViewBag.Sedes = new SelectList(db.Sedes, "Id", "Nombre");
            var programas = db.Database.SqlQuery<Programa>("ObtenerTodosLosProgramas").ToList();
            ViewBag.Programas = new SelectList(programas, "Id", "Nombre");

            var periodosAcademicos = db.Database.SqlQuery<PeriodoAcademico>("ObtenerTodosLosPeriodosAcademicos").ToList();
            ViewBag.PeriodosAcademicos = new SelectList(periodosAcademicos, "Id", "Periodo");

            return View(aspiranteDto);
        }

    }
}