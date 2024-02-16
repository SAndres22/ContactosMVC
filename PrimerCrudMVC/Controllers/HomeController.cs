using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerCrudMVC.Datos;
using PrimerCrudMVC.Models;
using System.Diagnostics;

namespace PrimerCrudMVC.Controllers
{
    //este es el la clase HomeController que extiende de la clase Controller
    public class HomeController : Controller
    {

        //Este atributo solo es de lectura
        private readonly AplicationDBContext _contexto;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //este es el constructor de HomeController que recibe un parametro de tipo AplicationDBContext
        public HomeController(AplicationDBContext contexto, IWebHostEnvironment hostingEnvironment)
        {
            _contexto = contexto;
            _hostingEnvironment = hostingEnvironment;
        }

        //Atraves de la url http://localhost:5000/Home/Index se puede acceder al controlador Home
        //y al metodo Index retornando la vista Index
        //Asyn: indica que la accion es asincrona es decir que se puede ejecutar en paralelo con otras acciones se debe usar el await
        //Task: indica el tipo de retorno representa el resultado de una accion como una vista o redireccion etc
        //await: indica que la accion es asincrona esto retorna una lista de contactos que se obtiene de la base de datos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contactos = await _contexto.Contactos
                               .OrderBy(c => c.nombre)
                               .ToListAsync();
            return View(contactos);
        }

        //Metodo para ir a la vista de crear un nuevo contacto
        [HttpGet]
        public IActionResult crearContacto()
        {
            return View();
        }

        //Metodo para crear un nuevo contacto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> crearContacto(Contacto contacto, IFormFile fotografia)
        {
            if (ModelState.IsValid)
            {
                // Guardar la fotografía en el servidor
                if (fotografia != null && fotografia.Length > 0)
                {
                    // Directorio donde se guardarán las imágenes
                    var uploadsDir = Path.Combine(_hostingEnvironment.WebRootPath, "Images");

                    // Asegurarse de que el directorio exista, si no, créalo
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    // Generar un nombre único para la imagen
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fotografia.FileName);

                    // Ruta completa donde se guardará la imagen
                    var filePath = Path.Combine(uploadsDir, fileName);

                    // Guardar la imagen en el servidor
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fotografia.CopyToAsync(fileStream);
                    }

                    // Asignar la ruta de la imagen al objeto Contacto
                    contacto.fotografia = "/Images/" + fileName;
                }
                //obtener la fecha de creacion del contacto
                contacto.fecha_creacion = DateTime.Now;

                // Añadir el contacto a tu contexto de base de datos
                _contexto.Contactos.Add(contacto);

                // Guardar los cambios en el contexto de la base de datos
                await _contexto.SaveChangesAsync();

                // Redirigir al usuario a la página de índice u otra página
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Si el modelo no es válido, devolver la vista con el modelo para que el usuario pueda corregir los errores
                return View(contacto);
            }
        }


        //Metodo para editar
        [HttpGet]
        public IActionResult editarContacto(int? id)
        {
            if(id == null){
                return NotFound();
            }

            var contacto = _contexto.Contactos.Find(id);
            if(contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        //Metodo para editar un contacto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editarContacto(Contacto contacto, IFormFile fotografia)
        {
            if (ModelState.IsValid)
            {
                // Guardar la fotografía en el servidor
                if (fotografia != null && fotografia.Length > 0)
                {
                    // Directorio donde se guardarán las imágenes
                    var uploadsDir = Path.Combine(_hostingEnvironment.WebRootPath, "Images");

                    // Asegurarse de que el directorio exista, si no, créalo
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    // Generar un nombre único para la imagen
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fotografia.FileName);

                    // Ruta completa donde se guardará la imagen
                    var filePath = Path.Combine(uploadsDir, fileName);

                    // Guardar la imagen en el servidor
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fotografia.CopyToAsync(fileStream);
                    }

                    // Asignar la ruta de la imagen al objeto Contacto
                    contacto.fotografia = "/Images/" + fileName;
                }

                // Añadir el update
                _contexto.Update(contacto);

                // Guardar los cambios en el contexto de la base de datos
                await _contexto.SaveChangesAsync();

                // Redirigir al usuario a la página de índice u otra página
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Si el modelo no es válido, devolver la vista con el modelo para que el usuario pueda corregir los errores
                return View(contacto);
            }
        }

        //Metodo para borrar Contacto
        [HttpGet]
        public async Task<IActionResult> borrarContacto(int? id)
        {
            var contacto = await _contexto.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return View();
            }

            // Eliminar la imagen asociada al contacto, si existe
            if (!string.IsNullOrEmpty(contacto.fotografia))
            {
                // Limpiar el nombre del archivo antes de combinarlo con la ruta
                var nombreArchivo = contacto.fotografia;
                var raiz = _hostingEnvironment.WebRootPath.ToString().Replace("\\", "/");
                var rutaImagen = raiz + nombreArchivo;
                if (System.IO.File.Exists(rutaImagen))
                {
                    System.IO.File.Delete(rutaImagen);
                }
                else
                {
                    Console.WriteLine("No existe la ruta " + rutaImagen);
                }
            }

            _contexto.Contactos.Remove(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
