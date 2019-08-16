using System;
using System.Collections.Generic;
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
    public class MensagemController : Controller
    {
        private readonly BlogContext _context;

        public MensagemController(BlogContext context)
        {
            _context = context;
        }

        // GET: Mensagem
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var blogContext = _context.Mensagem.Include(m => m.Categoria);
            var blogContext1 = _context.Comentario.Include(c => c.Mensagem);

            var vm = new VmComentariosMensagem
            {
                Mensagens = await blogContext.ToListAsync(),
                Comentarios = await blogContext1.ToListAsync()
            };

            return View(vm);
        }

        // GET: Mensagem/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensagem = await _context.Mensagem
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mensagem == null)
            {
                return NotFound();
            }

            return View(mensagem);
        }

        // GET: Mensagem/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descricao");
            return View();
        }

        // POST: Mensagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Data,CategoriaId")] Mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                mensagem.Data = DateTime.Now;
                _context.Add(mensagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descricao", mensagem.CategoriaId);
            return View(mensagem);
        }

        // GET: Mensagem/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensagem = await _context.Mensagem.FindAsync(id);
            if (mensagem == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descricao", mensagem.CategoriaId);
            return View(mensagem);
        }

        // POST: Mensagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Data,CategoriaId")] Mensagem mensagem)
        {
            if (id != mensagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mensagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MensagemExists(mensagem.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descricao", mensagem.CategoriaId);
            return View(mensagem);
        }

        // GET: Mensagem/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensagem = await _context.Mensagem
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mensagem == null)
            {
                return NotFound();
            }

            return View(mensagem);
        }

        // POST: Mensagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mensagem = await _context.Mensagem.FindAsync(id);
            _context.Mensagem.Remove(mensagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MensagemExists(int id)
        {
            return _context.Mensagem.Any(e => e.Id == id);
        }
        public async Task<Comentario> ComentariosMensagem(Mensagem mensagem)
        {
            var comentario = await _context.Comentario
              .Include(c => c.Mensagem)
              .FirstOrDefaultAsync(c => c.MensagemId == mensagem.Id);
            ViewData["ComentarioId"] = new SelectList(_context.Comentario, "Id", "Descricao", mensagem.CategoriaId);
            return comentario;
        }
    }

}
