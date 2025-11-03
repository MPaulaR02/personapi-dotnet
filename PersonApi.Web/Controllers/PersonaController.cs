using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.ViewModels;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace PersonApi.Web.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaRepository _personas;

        public PersonaController(IPersonaRepository personas)
        {
            _personas = personas;
        }

        // GET: Persona
        public async Task<IActionResult> Index()
        {
            var personas = (await _personas.GetAllAsync())
                .Select(p => new PersonaViewModel
                {
                    Cc = p.Cc,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Genero = p.Genero,
                    Edad = p.Edad
                })
                .ToList();

            return View(personas);
        }

        // GET: Persona/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _personas.GetByIdWithRelationsAsync(id.Value);

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

                await _personas.AddAsync(persona);
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

            var persona = await _personas.GetByIdAsync(id.Value);
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
                var persona = await _personas.GetByIdAsync(id);
                if (persona == null)
                {
                    return NotFound();
                }

                persona.Nombre = viewModel.Nombre;
                persona.Apellido = viewModel.Apellido;
                persona.Genero = viewModel.Genero;
                persona.Edad = viewModel.Edad;

                await _personas.UpdateAsync(persona);
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

            var persona = await _personas.GetByIdAsync(id.Value);

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
            await _personas.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _personas.ExistsAsync(id).GetAwaiter().GetResult();
        }
    }
}

