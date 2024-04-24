using Microsoft.AspNetCore.Mvc;
using MvcPersonajesSeries.Models;
using MvcPersonajesSeries.Services;

namespace MvcPersonajesSeries.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceAPIPersonajes service;

        public PersonajesController(ServiceAPIPersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> BuscadorSerie()
        {
            List<string> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.CreatePersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int idpersonaje)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(idpersonaje);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Personaje personaje)
        {
            await this.service.UpdatePersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int idpersonaje)
        {
            await this.service.DeletePersonajeAsync(idpersonaje);
            return RedirectToAction("Index");
        }
    }
}
