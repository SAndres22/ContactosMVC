using System.ComponentModel.DataAnnotations;

namespace PrimerCrudMVC.Models
{
    //Primer modelo --Tabla en la BD
    public class Contacto
    {
        //Atajo prop
        //Llave primaria
        [Key]
        public int id_contacto { get; set; }
        [Required (ErrorMessage ="El campo no puede estar vacio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo no puede estar vacio")]
        public string celular { get; set; }
        [Required(ErrorMessage = "El campo no puede estar vacio")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "El campo no puede estar vacio")]
        public string email { get; set; }    
        public string? fotografia { get; set; }
        //[NotMapped]
        //public IFormFile? foto { get; set; }
        public DateTime fecha_creacion { get; set; }

    }
}
