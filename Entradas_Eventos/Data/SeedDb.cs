using Entradas_Eventos.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entradas_Eventos.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly int EntrancesNum = 5000;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            _context.Database.Migrate();
            await CheckEntrancesAsync();
            await CheckTicketsAsync();
        }

        private async Task CheckEntrancesAsync()
        {
            if (!_context.Entrances.Any())
            {
                _context.Entrances.Add(new Entrance { Description = "Norte" });
                _context.Entrances.Add(new Entrance { Description = "Sur" });
                _context.Entrances.Add(new Entrance { Description = "Oriental" });
                _context.Entrances.Add(new Entrance { Description = "Occidental" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckTicketsAsync()
        {
            if (!_context.Tickets.Any())
            {
                Entrance e = _context.Entrances.FirstOrDefault(c => c.Id == 1);

                for (int i = 1; i <= EntrancesNum; i++)
                {
                    _context.Tickets.Add(new Ticket { WasUsed = false, Entrance = e });
                }
               
                await _context.SaveChangesAsync();
            }
        }
    }
}
