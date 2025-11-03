using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.ViewModels;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace PersonApi.Web.Controllers
{
    public class ProfesionController : Controller
    {
        private readonly IProfesionRepository _profesiones;

        public ProfesionController(IProfesionRepository profesiones)
        {
            _profesiones = profesiones;
        }

        // GET: Profesion
        public async Task<IActionResult> Index()
        {
            var profesiones = (await _profesiones.GetAllAsync())
                .Select(p => new ProfesionViewModel
                {
                    Id = p.Id,
                    Nom = p.Nom,
                    Des = p.Des
                })
                .ToList();

            return View(profesiones);
        }

        // GET: Profesion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _profesiones.GetByIdAsync(id.Value);
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

                await _profesiones.AddAsync(profesion);
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

            var profesion = await _profesiones.GetByIdAsync(id.Value);
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
                var profesion = await _profesiones.GetByIdAsync(id);
                if (profesion == null)
                {
                    return NotFound();
                }

                profesion.Nom = viewModel.Nom;
                profesion.Des = viewModel.Des;

                await _profesiones.UpdateAsync(profesion);
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

            var profesion = await _profesiones.GetByIdAsync(id.Value);
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
            await _profesiones.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesionExists(int id)
        {
            return _profesiones.ExistsAsync(id).GetAwaiter().GetResult();
        }
    }
}

