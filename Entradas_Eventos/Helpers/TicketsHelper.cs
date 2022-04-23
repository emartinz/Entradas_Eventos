using Entradas_Eventos.Data;
using Entradas_Eventos.Data.Entities;
using Entradas_Eventos.Helpers.Interfaces;
using Entradas_Eventos.Models;

namespace Entradas_Eventos.Helpers
{
    public class TicketsHelper : ITicketsHelper
    {
        private readonly DataContext _context;

        public TicketsHelper(DataContext context)
        {
            _context = context;
        }
        public async Task<Ticket> GetTicketAsync(TicketViewModel model)
        {
            Ticket ticket = new()
            {
                WasUsed = model.WasUsed,
                Name = model.Name,
                Document = model.Document,
                Date = model.Date,
                Entrance = await _context.Entrances.FindAsync(model.EntranceId)
            };

            return ticket;
        }

        public async Task<Entrance> GetTicketEntranceByIdAsync(int id)
        {
            return await _context.Entrances.FindAsync(id);
        }
    }
}
