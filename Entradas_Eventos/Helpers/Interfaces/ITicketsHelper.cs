using Entradas_Eventos.Data.Entities;
using Entradas_Eventos.Models;

namespace Entradas_Eventos.Helpers.Interfaces
{
    public interface ITicketsHelper
    {
        Task<Ticket> GetTicketAsync(TicketViewModel model);
        Task<Entrance> GetTicketEntranceByIdAsync(int id);
    }
}
