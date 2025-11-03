using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Context;
using personapi_dotnet.Models.ViewModels;
using personapi_dotnet.Models.Entities;

namespace PersonApi.Web.Controllers
{
    public class EstudioController : Controller
    {
        private readonly PersonaDbContext _context;

        public EstudioController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: Estudio
        public async Task<IActionResult> Index()
        {
            var estudios = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .Select(e => new EstudioViewModel
                {
                    IdProf = e.IdProf,
                    CcPer = e.CcPer,
                    Fecha = e.Fecha,
                    Univer = e.Univer,
                    ProfesionNombre = e.IdProfNavigation.Nom,
                    PersonaNombre = e.CcPerNavigation.Nombre + " " + e.CcPerNavigation.Apellido
                })
                .ToListAsync();

            return View(estudios);
        }

        // GET: Estudio/Details/5
        public async Task<IActionResult> Details(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == idProf && m.CcPer == ccPer);

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
        public IActionResult Create()
        {
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Nombre");
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Nom");
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

                _context.Add(estudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Nombre", viewModel.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Nom", viewModel.IdProf);
            return View(viewModel);
        }

        // GET: Estudio/Edit/5
        public async Task<IActionResult> Edit(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _context.Estudios.FindAsync(idProf, ccPer);
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

            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Nombre", viewModel.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Nom", viewModel.IdProf);
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
                try
                {
                    var estudio = await _context.Estudios.FindAsync(idProf, ccPer);
                    if (estudio == null)
                    {
                        return NotFound();
                    }

                    estudio.Fecha = viewModel.Fecha;
                    estudio.Univer = viewModel.Univer;

                    _context.Update(estudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudioExists(viewModel.IdProf, viewModel.CcPer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Nombre", viewModel.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Nom", viewModel.IdProf);
            return View(viewModel);
        }

        // GET: Estudio/Delete/5
        public async Task<IActionResult> Delete(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == idProf && m.CcPer == ccPer);

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
            var estudio = await _context.Estudios.FindAsync(idProf, ccPer);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudioExists(int idProf, int ccPer)
        {
            return _context.Estudios.Any(e => e.IdProf == idProf && e.CcPer == ccPer);
        }
    }
}

