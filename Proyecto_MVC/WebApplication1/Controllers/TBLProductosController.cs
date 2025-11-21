using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TBLProductosController : Controller
    {
        // GET: TBLProductos
        public ActionResult Index()
        {
            Proyecto_MVC_BD db = new Proyecto_MVC_BD(); //Instanciar el contexto de la db
            var productos = db.Productos.ToList(); //Obtener la lista de productos desde la db
            return View(productos.ToList()); //Mandar la lista de productos a la vista
        }
    }
}