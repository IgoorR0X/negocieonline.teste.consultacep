using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using negocieonline.teste.consultacep.Models;

namespace negocieonline.teste.consultacep.Controllers
{
    public class EnderecoDBsController : Controller
    {
        private readonly EnderecoContext _context;

        public EnderecoDBsController(EnderecoContext context)
        {
            _context = context;
        }

        // GET: EnderecoDBs
        public async Task<IActionResult> Index()
        {
            return View(await _context.EnderecoDBs.ToListAsync());
        }

        // GET: EnderecoDBs/Details/5
        public async Task<IActionResult> Details(string? cep)
        {
            if (cep == null)
            {
                return NotFound();
            }

            var enderecoDB = await _context.EnderecoDBs
                .FirstOrDefaultAsync(m => m.cep == cep);
            if (enderecoDB == null)
            {
                return NotFound();
            }

            return View(enderecoDB);
        }

        // GET: EnderecoDBs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnderecoDBs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,cep,logradouro,complemento,bairro,localidade,uf,ibge,gia,ddd,siafi")] EnderecoDB enderecoDB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enderecoDB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enderecoDB);
        }

        // GET: EnderecoDBs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoDB = await _context.EnderecoDBs.FindAsync(id);
            if (enderecoDB == null)
            {
                return NotFound();
            }
            return View(enderecoDB);
        }

        // POST: EnderecoDBs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,cep,logradouro,complemento,bairro,localidade,uf,ibge,gia,ddd,siafi")] EnderecoDB enderecoDB)
        {
            if (id != enderecoDB.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enderecoDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoDBExists(enderecoDB.id))
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
            return View(enderecoDB);
        }

        // GET: EnderecoDBs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoDB = await _context.EnderecoDBs
                .FirstOrDefaultAsync(m => m.id == id);
            if (enderecoDB == null)
            {
                return NotFound();
            }

            return View(enderecoDB);
        }

        // POST: EnderecoDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enderecoDB = await _context.EnderecoDBs.FindAsync(id);
            _context.EnderecoDBs.Remove(enderecoDB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoDBExists(int id)
        {
            return _context.EnderecoDBs.Any(e => e.id == id);
        }
    }
}
