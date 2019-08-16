using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog161.Models;
using Blog161.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Blog161.Data;

namespace Blog161.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    [Authorize(Roles = "Administrador")]
    public class ComentarioController : Controller
    {
        private readonly BlogContext _context;

        public ComentarioController(BlogContext context)
        {
            _context = context;
        }

        // GET: Comentario
        public async Task<IActionResult> Index()
        {
            var blogContext = _context.Comentario.Include(c => c.Mensagem);
            return View(await blogContext.ToListAsync());
        }

        // GET: Comentario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario
                .Include(c => c.Mensagem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentario/Create
        public async Task<ActionResult> Create(int id)
        {
            var mensagem = await _context.Mensagem.FindAsync(id);
            ViewBag.Mensagem = mensagem;
            return View();
        }

        // POST: Comentario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,Titulo,Descricao,Data,Autor,MensagemId")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.MensagemId = id;
                comentario.Data = DateTime.Now;
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), nameof(Mensagem));
            }
            ViewData["MensagemId"] = new SelectList(_context.Mensagem, "Id", "Titulo", comentario.MensagemId);
            var vm = new VmComentariosMensagem();

            return View(vm);
        }

        // GET: Comentario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            ViewData["MensagemId"] = new SelectList(_context.Mensagem, "Id", "Titulo", comentario.MensagemId);
            return View(comentario);
        }

        // POST: Comentario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Data,Autor,MensagemId")] Comentario comentario)
        {
            if (id != comentario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.Id))
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
            ViewData["MensagemId"] = new SelectList(_context.Mensagem, "Id", "Titulo", comentario.MensagemId);
            return View(comentario);
        }

        // GET: Comentario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario
                .Include(c => c.Mensagem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // POST: Comentario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comentario = await _context.Comentario.FindAsync(id);
            _context.Comentario.Remove(comentario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioExists(int id)
        {
            return _context.Comentario.Any(e => e.Id == id);
        }
        
    }
}
