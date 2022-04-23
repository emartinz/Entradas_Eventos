#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entradas_Eventos.Data;
using Entradas_Eventos.Data.Entities;

namespace Entradas_Eventos.Controllers
{
    public class EntrancesController : Controller
    {
        private readonly DataContext _context;

        public EntrancesController(DataContext context)
        {
            _context = context;
        }

        // GET: Entrances
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Entrances.ToListAsync());
        }

        // GET: Entrances/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrance = await _context.Entrances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrance == null)
            {
                return NotFound();
            }

            return View(entrance);
        }

        // GET: Entrances/Create
        [HttpGet]
        public IActionResult Create()
        {
            Entrance entrance = new() { Tickets = new List<Ticket>() };
            return View(entrance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Entrance entrance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(entrance);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Entrance con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(entrance);
        }

        // GET: Entrances/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrance = await _context.Entrances.FindAsync(id);
            if (entrance == null)
            {
                return NotFound();
            }
            return View(entrance);
        }

        // POST: Entrances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Entrance entrance)
        {
            if (id != entrance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntranceExists(entrance.Id))
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
            return View(entrance);
        }

        // GET: Entrances/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrance = await _context.Entrances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrance == null)
            {
                return NotFound();
            }

            return View(entrance);
        }

        // POST: Entrances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrance = await _context.Entrances.FindAsync(id);
            _context.Entrances.Remove(entrance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntranceExists(int id)
        {
            return _context.Entrances.Any(e => e.Id == id);
        }
    }
}
