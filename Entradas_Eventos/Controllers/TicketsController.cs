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
using Entradas_Eventos.Helpers;
using Entradas_Eventos.Models;
using Entradas_Eventos.Helpers.Interfaces;

namespace Entradas_Eventos.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly ITicketsHelper _ticketsHelper;

        public TicketsController(DataContext context, ICombosHelper combosHelper, ITicketsHelper ticketsHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _ticketsHelper = ticketsHelper;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TicketViewModel model = new();
            Ticket ticket = new();
            if (id != null)
            {
                ticket = await _context.Tickets.Include(ticket => ticket.Entrance).FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                ticket = await _context.Tickets.Include(ticket => ticket.Entrance).Where(p => p.WasUsed.Equals(false)).FirstOrDefaultAsync();
            }

            if (null != ticket)
            {
                model = await _ticketsHelper.GetModelFromTicketAsync(ticket);
            }

            return View(model);
        }

        // GET: Tickets/DetailsTicket/5
        public async Task<IActionResult> DetailsTicket(int? id)
        {
            TicketViewModel model = new();
            Ticket ticket = new();
            if (id != null)
            {
                ticket = await _context.Tickets.Include(ticket => ticket.Entrance).FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                ticket = await _context.Tickets.Include(ticket => ticket.Entrance).Where(p => p.WasUsed.Equals(false)).FirstOrDefaultAsync();
            }

            if (null != ticket)
            {
                model = await _ticketsHelper.GetModelFromTicketAsync(ticket);
            }

            return View(model);
        }

        // GET: Tickets/AssignTicket
        [HttpGet]
        public async Task<IActionResult> AssignTicketAsync(int? id)
        {
            TicketViewModel model = new();
            Ticket ticket = new();
            if (id != null)
            {
                ticket = await _context.Tickets.Include(ticket => ticket.Entrance).FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                ticket = await _context.Tickets.Include(ticket => ticket.Entrance).Where(p => p.WasUsed.Equals(false)).FirstOrDefaultAsync();
            }
            
            if (null != ticket) {
                model = await _ticketsHelper.GetModelFromTicketAsync(ticket);
            }
            else {
                ModelState.AddModelError(string.Empty, "No hay entradas disponibles.");
            }
            model.EntrancesList = await _combosHelper.GetComboEntranceAsync();
            model.WasUsed = true;
            model.Date = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignTicket(TicketViewModel model)
        {
            model.entrance = await _ticketsHelper.GetTicketEntranceByIdAsync(model.EntranceId);

            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = await _ticketsHelper.GetTicketFromModelAsync(model);
                    _context.Update(ticket);
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
            return View(model);
        }

        // GET: Tickets/SeekTicket
        [HttpGet]
        public async Task<IActionResult> SeekTicketAsync()
        {
            TicketViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SeekTicketAsync(TicketViewModel model)
        {

            if (model.Id != null)
            {
                var ticket = await _context.Tickets.Include(ticket => ticket.Entrance).FirstOrDefaultAsync(m => m.Id == model.Id);
                if (ticket == null)
                {
                    ModelState.AddModelError(string.Empty, "Esta boleta no existe.");
                }
                else if (ticket.WasUsed)
                {
                    return RedirectToAction("DetailsTicket", new { id = model.Id });
                }
                else
                {
                    return RedirectToAction("AssignTicket", new { id = model.Id });
                }
            }
            return View(model);
        }


        

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(TicketViewModel model)
        {
            Entrance entrance = await _ticketsHelper.GetTicketEntranceByIdAsync(model.EntranceId);
            model.entrance = entrance;
            return View(model);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
