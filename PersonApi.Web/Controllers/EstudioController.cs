using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.ViewModels;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace PersonApi.Web.Controllers
{
    public class EstudioController : Controller
    {
        private readonly IEstudioRepository _estudios;
        private readonly IPersonaRepository _personas;
        private readonly IProfesionRepository _profesiones;

        public EstudioController(IEstudioRepository estudios, IPersonaRepository personas, IProfesionRepository profesiones)
        {
            _estudios = estudios;
            _personas = personas;
            _profesiones = profesiones;
        }

        // GET: Estudio
        public async Task<IActionResult> Index()
        {
            var estudios = (await _estudios.GetAllAsync())
                .Select(e => new EstudioViewModel
                {
                    IdProf = e.IdProf,
                    CcPer = e.CcPer,
                    Fecha = e.Fecha,
                    Univer = e.Univer,
                    ProfesionNombre = e.IdProfNavigation.Nom,
                    PersonaNombre = e.CcPerNavigation.Nombre + " " + e.CcPerNavigation.Apellido
                })
                .ToList();

            return View(estudios);
        }

        // GET: Estudio/Details/5
        public async Task<IActionResult> Details(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudios.GetByKeyWithRelationsAsync(idProf.Value, ccPer.Value);

            if (estudio == null)
            {
                return NotFound();
            }

            var viewModel = new EstudioViewModel
            {
                IdProf = estudio.IdProf,
                CcPer = estudio.CcPer,
                Fecha = estudio.Fecha,
                Univer = estudio.Univer,
                ProfesionNombre = estudio.IdProfNavigation.Nom,
                PersonaNombre = estudio.CcPerNavigation.Nombre + " " + estudio.CcPerNavigation.Apellido
            };

            return View(viewModel);
        }

        // GET: Estudio/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CcPer"] = new SelectList(await _personas.GetAllAsync(), "Cc", "Nombre");
            ViewData["IdProf"] = new SelectList(await _profesiones.GetAllAsync(), "Id", "Nom");
            return View();
        }

        // POST: Estudio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] EstudioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var estudio = new Estudio
                {
                    IdProf = viewModel.IdProf,
                    CcPer = viewModel.CcPer,
                    Fecha = viewModel.Fecha,
                    Univer = viewModel.Univer
                };

                await _estudios.AddAsync(estudio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CcPer"] = new SelectList(await _personas.GetAllAsync(), "Cc", "Nombre", viewModel.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesiones.GetAllAsync(), "Id", "Nom", viewModel.IdProf);
            return View(viewModel);
        }

        // GET: Estudio/Edit/5
        public async Task<IActionResult> Edit(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudios.GetByKeyAsync(idProf.Value, ccPer.Value);
            if (estudio == null)
            {
                return NotFound();
            }

            var viewModel = new EstudioViewModel
            {
                IdProf = estudio.IdProf,
                CcPer = estudio.CcPer,
                Fecha = estudio.Fecha,
                Univer = estudio.Univer
            };

            ViewData["CcPer"] = new SelectList(await _personas.GetAllAsync(), "Cc", "Nombre", viewModel.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesiones.GetAllAsync(), "Id", "Nom", viewModel.IdProf);
            return View(viewModel);
        }

        // POST: Estudio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, [Bind("IdProf,CcPer,Fecha,Univer")] EstudioViewModel viewModel)
        {
            if (idProf != viewModel.IdProf || ccPer != viewModel.CcPer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var estudio = await _estudios.GetByKeyAsync(idProf, ccPer);
                if (estudio == null)
                {
                    return NotFound();
                }

                estudio.Fecha = viewModel.Fecha;
                estudio.Univer = viewModel.Univer;

                await _estudios.UpdateAsync(estudio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CcPer"] = new SelectList(await _personas.GetAllAsync(), "Cc", "Nombre", viewModel.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesiones.GetAllAsync(), "Id", "Nom", viewModel.IdProf);
            return View(viewModel);
        }

        // GET: Estudio/Delete/5
        public async Task<IActionResult> Delete(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudios.GetByKeyWithRelationsAsync(idProf.Value, ccPer.Value);

            if (estudio == null)
            {
                return NotFound();
            }

            var viewModel = new EstudioViewModel
            {
                IdProf = estudio.IdProf,
                CcPer = estudio.CcPer,
                Fecha = estudio.Fecha,
                Univer = estudio.Univer,
                ProfesionNombre = estudio.IdProfNavigation.Nom,
                PersonaNombre = estudio.CcPerNavigation.Nombre + " " + estudio.CcPerNavigation.Apellido
            };

            return View(viewModel);
        }

        // POST: Estudio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            await _estudios.DeleteAsync(idProf, ccPer);
            return RedirectToAction(nameof(Index));
        }

        private bool EstudioExists(int idProf, int ccPer)
        {
            return _estudios.ExistsAsync(idProf, ccPer).GetAwaiter().GetResult();
        }
    }
}

