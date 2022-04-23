using Entradas_Eventos.Data.Entities;
using Entradas_Eventos.Models;

namespace Entradas_Eventos.Helpers.Interfaces
{
    public interface ITicketsHelper
    {
        Task<Ticket> GetTicketFromModelAsync(TicketViewModel model);
        Task<Entrance> GetTicketEntranceByIdAsync(int id);
        Task<TicketViewModel> GetModelFromTicketAsync(Ticket ticket);
    }
}
