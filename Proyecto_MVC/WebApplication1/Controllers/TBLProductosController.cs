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

        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Proyecto_MVC_BD db = new Proyecto_MVC_BD();
                    Productos producto = new Productos();
                    producto.NombreProducto = collection["NombreProducto"];
                    producto.Descripcion = collection["Descripcion"];
                    producto.Precio = Convert.ToDecimal(collection["Precio"]);
                    producto.Stock = Convert.ToInt32(collection["Stock"]);
                    
                    db.Productos.Add(producto);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar (int id)
        {
            Proyecto_MVC_BD db = new Proyecto_MVC_BD();
            var producto = db.Productos.Find(id); //buscar el producto por id
            return View(producto);
        }
        [HttpPost]
        public ActionResult Editar(Productos model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Proyecto_MVC_BD db = new Proyecto_MVC_BD();
                    var producto = db.Productos.Find(model.IdProducto);
                    producto.NombreProducto = model.NombreProducto;
                    producto.Descripcion = model.Descripcion;
                    producto.Precio = Convert.ToDecimal(model.Precio);
                    producto.Stock = Convert.ToInt32(model.Stock);

                    db.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            try
            {
                using (Proyecto_MVC_BD db = new Proyecto_MVC_BD())
                {
                    var producto = db.Productos.Find(id);
                    db.Productos.Remove(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }
    }
}