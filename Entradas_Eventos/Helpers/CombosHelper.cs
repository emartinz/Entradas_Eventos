using Entradas_Eventos.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Entradas_Eventos.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<SelectListItem>> GetComboEntranceAsync()
        {
            List<SelectListItem> list = await _context.Entrances.Select(c => new SelectListItem
            {
                Text = c.Description,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Seleccione un Entrance...", Value = "0" });
            return list;
        }
    }
}
