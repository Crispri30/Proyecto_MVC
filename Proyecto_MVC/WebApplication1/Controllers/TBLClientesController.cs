using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TBLClientesController : Controller
    {
        // GET: TBLClientes
        public ActionResult Index()
        {
            Proyecto_MVC_BD db = new Proyecto_MVC_BD(); //Conectar a la bd

            var clientes = db.Clientes; //Traer la tabla empleados

            return View(clientes.ToList()); //Enviar la lista de empleados a la vista
        }
        public ActionResult Nuevo() //Se usa action result para devolver una vista
        {
            return View();
        }
    }
}