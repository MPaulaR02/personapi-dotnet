using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Context;
using personapi_dotnet.Models.ViewModels;
using personapi_dotnet.Models.Entities;

namespace PersonApi.Web.Controllers
{
    public class PersonaController : Controller
    {
        private readonly PersonaDbContext _context;

        public PersonaController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: Persona
        public async Task<IActionResult> Index()
        {
            var personas = await _context.Personas
                .Select(p => new PersonaViewModel
                {
                    Cc = p.Cc,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Genero = p.Genero,
                    Edad = p.Edad
                })
                .ToListAsync();

            return View(personas);
        }

        // GET: Persona/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .Include(p => p.Telefonos)
                .Include(p => p.Estudios)
                .FirstOrDefaultAsync(m => m.Cc == id);

            if (persona == null)
            {
                return NotFound();
            }

            var viewModel = new PersonaViewModel
            {
                Cc = persona.Cc,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Genero = persona.Genero,
                Edad = persona.Edad,
                Telefonos = persona.Telefonos.Select(t => new TelefonoViewModel
                {
                    Num = t.Num,
                    Oper = t.Oper,
                    Duenio = t.Duenio
                }).ToList(),
                Estudios = persona.Estudios.Select(e => new EstudioViewModel
                {
                    IdProf = e.IdProf,
                    CcPer = e.CcPer,
                    Fecha = e.Fecha,
                    Univer = e.Univer
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: Persona/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cc,Nombre,Apellido,Genero,Edad")] PersonaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var persona = new Persona
                {
                    Cc = viewModel.Cc,
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    Genero = viewModel.Genero,
                    Edad = viewModel.Edad
                };

                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Persona/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            var viewModel = new PersonaViewModel
            {
                Cc = persona.Cc,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Genero = persona.Genero,
                Edad = persona.Edad
            };

            return View(viewModel);
        }

        // POST: Persona/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cc,Nombre,Apellido,Genero,Edad")] PersonaViewModel viewModel)
        {
            if (id != viewModel.Cc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var persona = await _context.Personas.FindAsync(id);
                    if (persona == null)
                    {
                        return NotFound();
                    }

                    persona.Nombre = viewModel.Nombre;
                    persona.Apellido = viewModel.Apellido;
                    persona.Genero = viewModel.Genero;
                    persona.Edad = viewModel.Edad;

                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(viewModel.Cc))
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

        // GET: Persona/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Cc == id);

            if (persona == null)
            {
                return NotFound();
            }

            var viewModel = new PersonaViewModel
            {
                Cc = persona.Cc,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Genero = persona.Genero,
                Edad = persona.Edad
            };

            return View(viewModel);
        }

        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Cc == id);
        }
    }
}

