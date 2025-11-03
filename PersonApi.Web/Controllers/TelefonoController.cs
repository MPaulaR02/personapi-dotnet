using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Context;
using personapi_dotnet.Models.ViewModels;
using personapi_dotnet.Models.Entities;

namespace PersonApi.Web.Controllers
{
    public class TelefonoController : Controller
    {
        private readonly PersonaDbContext _context;

        public TelefonoController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: Telefono
        public async Task<IActionResult> Index()
        {
            var telefonos = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .Select(t => new TelefonoViewModel
                {
                    Num = t.Num,
                    Oper = t.Oper,
                    Duenio = t.Duenio,
                    PersonaNombre = t.DuenioNavigation.Nombre + " " + t.DuenioNavigation.Apellido
                })
                .ToListAsync();

            return View(telefonos);
        }

        // GET: Telefono/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);

            if (telefono == null)
            {
                return NotFound();
            }

            var viewModel = new TelefonoViewModel
            {
                Num = telefono.Num,
                Oper = telefono.Oper,
                Duenio = telefono.Duenio,
                PersonaNombre = telefono.DuenioNavigation.Nombre + " " + telefono.DuenioNavigation.Apellido
            };

            return View(viewModel);
        }

        // GET: Telefono/Create
        public IActionResult Create()
        {
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Nombre");
            return View();
        }

        // POST: Telefono/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Oper,Duenio")] TelefonoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var telefono = new Telefono
                {
                    Num = viewModel.Num,
                    Oper = viewModel.Oper,
                    Duenio = viewModel.Duenio
                };

                _context.Add(telefono);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Nombre", viewModel.Duenio);
            return View(viewModel);
        }

        // GET: Telefono/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            var viewModel = new TelefonoViewModel
            {
                Num = telefono.Num,
                Oper = telefono.Oper,
                Duenio = telefono.Duenio
            };

            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Nombre", viewModel.Duenio);
            return View(viewModel);
        }

        // POST: Telefono/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Num,Oper,Duenio")] TelefonoViewModel viewModel)
        {
            if (id != viewModel.Num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var telefono = await _context.Telefonos.FindAsync(id);
                    if (telefono == null)
                    {
                        return NotFound();
                    }

                    telefono.Oper = viewModel.Oper;
                    telefono.Duenio = viewModel.Duenio;

                    _context.Update(telefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(viewModel.Num))
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
            ViewData["Duenio"] = new SelectList(_context.Personas, "Cc", "Nombre", viewModel.Duenio);
            return View(viewModel);
        }

        // GET: Telefono/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .Include(t => t.DuenioNavigation)
                .FirstOrDefaultAsync(m => m.Num == id);

            if (telefono == null)
            {
                return NotFound();
            }

            var viewModel = new TelefonoViewModel
            {
                Num = telefono.Num,
                Oper = telefono.Oper,
                Duenio = telefono.Duenio,
                PersonaNombre = telefono.DuenioNavigation.Nombre + " " + telefono.DuenioNavigation.Apellido
            };

            return View(viewModel);
        }

        // POST: Telefono/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelefonoExists(string id)
        {
            return _context.Telefonos.Any(e => e.Num == id);
        }
    }
}

