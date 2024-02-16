using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrimerCrudMVC.Models
{
    public class Direccion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo no puede estar vacío")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "La longitud de la calle debe estar entre 2 y 100 caracteres")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El campo no puede estar vacío")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "La longitud de la ciudad debe estar entre 2 y 50 caracteres")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El campo no puede estar vacío")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "La longitud del país debe estar entre 2 y 50 caracteres")]
        public string Pais { get; set; }

        // Propiedad de navegación para establecer la relación con Contacto
        [ForeignKey("Contacto")]
        public int ContactoId { get; set; }
        public Contacto Contacto { get; set; }
    }
}
