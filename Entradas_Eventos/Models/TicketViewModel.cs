using Entradas_Eventos.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entradas_Eventos.Models
{
    public class TicketViewModel
    {

        public int? Id { get; set; }

        [Display(Name = "Usada?")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool WasUsed { get; set; } = false;

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Ubicacion")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una Ubicacion.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int EntranceId { get; set; }

        public IEnumerable<SelectListItem>? EntrancesList { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Entrance entrance;

        [Display(Name = "Fecha y Hora")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }
    }
}
