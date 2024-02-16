using Microsoft.EntityFrameworkCore;
using PrimerCrudMVC.Models;

namespace PrimerCrudMVC.Datos
{
    //Creamos la clase AplicationDBContext que hereda de DbContext que es la encargada de la base de datos
    public class AplicationDBContext : DbContext
    {

        //Constructor vacio
        public AplicationDBContext() { }
        //Constructor
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options)
        {

        }

        //Aca van los modelos en ESTE CASO EL MODELO CONTACTO
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
    }
}
