using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.ViewModels;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace PersonApi.Web.Controllers
{
    public class TelefonoController : Controller
    {
        private readonly ITelefonoRepository _telefonos;
        private readonly IPersonaRepository _personas;

        public TelefonoController(ITelefonoRepository telefonos, IPersonaRepository personas)
        {
            _telefonos = telefonos;
            _personas = personas;
        }

        // GET: Telefono
        public async Task<IActionResult> Index()
        {
            var telefonos = (await _telefonos.GetAllAsync())
                .Select(t => new TelefonoViewModel
                {
                    Num = t.Num,
                    Oper = t.Oper,
                    Duenio = t.Duenio,
                    PersonaNombre = t.DuenioNavigation.Nombre + " " + t.DuenioNavigation.Apellido
                })
                .ToList();

            return View(telefonos);
        }

        // GET: Telefono/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonos.GetByNumAsync(id);

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
        public async Task<IActionResult> Create()
        {
            ViewData["Duenio"] = new SelectList(await _personas.GetAllAsync(), "Cc", "Nombre");
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

                await _telefonos.AddAsync(telefono);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Duenio"] = new SelectList(await _personas.GetAllAsync(), "Cc", "Nombre", viewModel.Duenio);
            return View(viewModel);
        }

        // GET: Telefono/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonos.GetByNumAsync(id);
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

            ViewData["Duenio"] = new SelectList(await _personas.GetAllAsync(), "Cc", "Nombre", viewModel.Duenio);
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
                var telefono = await _telefonos.GetByNumAsync(id);
                if (telefono == null)
                {
                    return NotFound();
                }

                telefono.Oper = viewModel.Oper;
                telefono.Duenio = viewModel.Duenio;

                await _telefonos.UpdateAsync(telefono);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Duenio"] = new SelectList(await _personas.GetAllAsync(), "Cc", "Nombre", viewModel.Duenio);
            return View(viewModel);
        }

        // GET: Telefono/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonos.GetByNumAsync(id);

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
            await _telefonos.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TelefonoExists(string id)
        {
            return _telefonos.ExistsAsync(id).GetAwaiter().GetResult();
        }
    }
}

