using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Context;
using personapi_dotnet.Models.ViewModels;
using personapi_dotnet.Models.Entities;

namespace PersonApi.Web.Controllers
{
    public class ProfesionController : Controller
    {
        private readonly PersonaDbContext _context;

        public ProfesionController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: Profesion
        public async Task<IActionResult> Index()
        {
            var profesiones = await _context.Profesions
                .Select(p => new ProfesionViewModel
                {
                    Id = p.Id,
                    Nom = p.Nom,
                    Des = p.Des
                })
                .ToListAsync();

            return View(profesiones);
        }

        // GET: Profesion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            var viewModel = new ProfesionViewModel
            {
                Id = profesion.Id,
                Nom = profesion.Nom,
                Des = profesion.Des
            };

            return View(viewModel);
        }

        // GET: Profesion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Des")] ProfesionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profesion = new Profesion
                {
                    Id = viewModel.Id,
                    Nom = viewModel.Nom,
                    Des = viewModel.Des
                };

                _context.Add(profesion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Profesion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            var viewModel = new ProfesionViewModel
            {
                Id = profesion.Id,
                Nom = profesion.Nom,
                Des = profesion.Des
            };

            return View(viewModel);
        }

        // POST: Profesion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Des")] ProfesionViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var profesion = await _context.Profesions.FindAsync(id);
                    if (profesion == null)
                    {
                        return NotFound();
                    }

                    profesion.Nom = viewModel.Nom;
                    profesion.Des = viewModel.Des;

                    _context.Update(profesion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionExists(viewModel.Id))
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
            return View(viewModel);
        }

        // GET: Profesion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            var viewModel = new ProfesionViewModel
            {
                Id = profesion.Id,
                Nom = profesion.Nom,
                Des = profesion.Des
            };

            return View(viewModel);
        }

        // POST: Profesion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion != null)
            {
                _context.Profesions.Remove(profesion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesionExists(int id)
        {
            return _context.Profesions.Any(e => e.Id == id);
        }
    }
}

