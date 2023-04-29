using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ultimaPract.Models;

namespace ultimaPract.Controllers
{
    public class equiposController : Controller
    {
        private readonly equiposContext _equiposContext;
        public equiposController(equiposContext equiposContext)
        {
            _equiposContext= equiposContext;
        }
        public IActionResult Index()
        {
            var listadoDeMarcas = (from m in _equiposContext.marcas select m).ToList();
            //manejador  de control y vista
            ViewData["listadoDeMarcas"] = new SelectList(listadoDeMarcas, "id_marcas", "nombre");
            var listadoTipoEquipo = (from tp in _equiposContext.tipo_equipo select tp).ToList();
            ViewData["listadoTipoEquipo"] = new SelectList(listadoTipoEquipo, "id_tipo_equipo", "descripcion");
            var listadoEstadoEquipo = (from eq in _equiposContext.estados_equipo select eq).ToList();
            ViewData[" listadoEstadoEquipo"] = new SelectList(listadoEstadoEquipo, "id_estados_equipos", "descripcion");
            var listadoDeEquipos = (from e in _equiposContext.equipos join m in _equiposContext.marcas
                                   on e.marca_id equals m.id_marcas 
                                   select new
                                   {
                                       nombre = e.nombre,
                                       descripcion = e.descripcion,
                                       marca_id= e.marca_id,
                                       marca_nombre= m.nombre_marca,
                                   }).ToList();
            ViewData["listadoDeEquipos"] = listadoDeEquipos;

            return View();
        }
        public IActionResult crearRegistro(equipos nuevoEquipo) 
        {
            _equiposContext.Add(nuevoEquipo);
            _equiposContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
