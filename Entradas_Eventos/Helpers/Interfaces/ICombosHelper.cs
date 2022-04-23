using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entradas_Eventos.Helpers
{
    public interface ICombosHelper
    {

        Task<IEnumerable<SelectListItem>> GetComboEntranceAsync();

    }
}
