﻿using System.ComponentModel.DataAnnotations;

namespace Entradas_Eventos.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        [Display(Name = "Usada?")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool WasUsed { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public String Document { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Name { get; set; }

        public Entrance Entrance { get; set; }

        public DateTime Date { get; set; }

    }
}
