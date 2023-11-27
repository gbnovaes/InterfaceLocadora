using Locadora.Data;
using Locadora.Interfaces;
using Locadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Locadora.Controllers
{
    public class FilmeController : Controller
    {
        private readonly ILocadoraContext _context;
        public FilmeController(ILocadoraContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Filmes.OrderBy(i => i.Nome).ToListAsync());
        }
        
        //private static IList<Filme> filmes = new List<Filme>()
        //{
        //    new Filme()
        //    {
        //        FilmeID = 1,
        //        Nome = "Harry Potter",
        //        Genero = "Fantasia",
        //        Sobre = "Magia"
        //    }
        //};
        //public IActionResult Index()
        //{
        //    ViewData["Title"] = "Catálogo de Filmes";
        //    return View(filmes);
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome, Genero, Sobre")] Filme filme)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.AddFilmeAsync(filme);
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("Erro de cadastro", "Não foi possível cadastrar a instituição.");
            }

            return View(filme);
        }


        //public ActionResult Create(Filme filme)
        //{
        //    filme.FilmeID = filmes.Select(i => i.FilmeID).Max() + 1;
        //    filmes.Add(filme);
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Edit(long id)
        //{
        //    return View(filmes.Where(i => i.FilmeID == id).First());
        //}


        public async Task<ActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var filme = await _context.GetFilmeByIdAsync(id); 
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("FilmeID", "Nome", "Genero", "Sobre")] Filme filme)
        {
            if (id != filme.FilmeID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateFilmeAsync(filme);
                }
                catch (DbUpdateException)
                {
                    if (!FilmeExists(filme.FilmeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(filme);
        }

        private bool FilmeExists(long? filmeID)
        {
            throw new NotImplementedException();
        }

        //public ActionResult Edit(Filme filme)
        //{
        //    filmes.Remove(filmes.Where(i => i.FilmeID == filme.FilmeID).First());
        //    filmes.Add(filme);
        //    return RedirectToAction("Index");
        //}
        public async Task<IActionResult> Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var filme = await _context.GetFilmeByIdAsync(id.Value);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }
        //public IActionResult Details(long id)
        //{
        //    return View(filmes.Where(i => i.FilmeID == id).First());
        //}
        public async Task<IActionResult> Delete(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var filme = await _context.GetFilmeByIdAsync(id.Value);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }
        //public IActionResult Delete(long id)
        //{
        //    return View(filmes.Where(i => i.FilmeID == id).First());
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            var filme = await _context.GetFilmeByIdAsync(id.Value);
            await _context.DeleteFilmeAsync(id.Value);
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(Filme filme)
        //{
        //    filmes.Remove(filmes.Where(i => i.FilmeID == filme.FilmeID).First());
        //    return RedirectToAction("Index");
        //}
    }
}
