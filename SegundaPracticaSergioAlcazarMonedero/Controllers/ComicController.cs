using Microsoft.AspNetCore.Mvc;
using SegundaPracticaSergioAlcazarMonedero.Models;
using SegundaPracticaSergioAlcazarMonedero.Repositories;

namespace SegundaPracticaSergioAlcazarMonedero.Controllers
{
    public class ComicController : Controller
    {
        IRepository repo;
        public ComicController(IRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Comic comic)
        {
            this.repo.InsertarComic(comic.Nombre, comic.Imagen, comic.Descripcion);
            return RedirectToAction("Index");
        }
    }
}
