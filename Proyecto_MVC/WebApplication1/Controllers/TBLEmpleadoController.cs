using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TBLEmpleadoController : Controller
    {
        // GET: TBLEmpleado
        public ActionResult Index()
        {
            Proyecto_MVC_BD db = new Proyecto_MVC_BD(); // Instanciar el contexto de la bd
            var empleados = db.Empleados.ToList(); //Obtener todos los empleados de la tabla empleados
            return View(empleados.ToList()); //Mostrar al index la lista de empleados
        }
        public ActionResult Nuevo() //Se usa action result para devolver una vista
        {
            return View();
        }
        [HttpPost] //indica que este metodo responde a una peticion de la web vía post
        public ActionResult Nuevo(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Proyecto_MVC_BD db = new Proyecto_MVC_BD(); //Conectar a la bd
                    var empleado = new Empleados(); //Crear un objeto de tipo empleado
                    empleado.Nombre = collection["Nombre"]; //Asignar valores al objeto empleado
                    empleado.Documento = collection["Documento"];
                    empleado.Email = collection["Email"];
                    empleado.FechaIngreso = Convert.ToDateTime(collection["FechaIngreso"]);

                    db.Empleados.Add(empleado); //Agregar el objeto a la tabla empleados
                    db.SaveChanges(); //Guardar los cambios en la bd

                    return RedirectToAction("Index"); //Redirigir al index
                }
                return View(collection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            Proyecto_MVC_BD db = new Proyecto_MVC_BD(); //conectar a la bd
            var empleado = db.Empleados.Find(id); //Buscar el empleado por id

            return View(empleado);
        }
        [HttpPost]
        public ActionResult Editar(Empleados model) //Recibir el modelo de empleado modificado de la vista
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Proyecto_MVC_BD db = new Proyecto_MVC_BD())
                    {
                        var empleadomodi = db.Empleados.Find(model.EmpleadoID); //Buscar el empleado por id
                        //Actualizar los valores del empleado
                        empleadomodi.Nombre = model.Nombre;
                        empleadomodi.Documento = model.Documento;
                        empleadomodi.Email = model.Email;
                        empleadomodi.FechaIngreso = model.FechaIngreso;

                        db.Entry(empleadomodi).State = System.Data.Entity.EntityState.Modified; //Marcar el objeto como modificado
                        db.SaveChanges(); //Guardar los cambios en la bd
                    }
                    return RedirectToAction("Index"); //Redirigir al index
                }
                return View(model); //Si el modelo no es valido, regresar la vista con el modelo
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
                    var empleado = db.Empleados.Find(id); //Buscar el empleado por id
                    db.Empleados.Remove(empleado); //Eliminar el empleado
                    db.SaveChanges(); //Guardar los cambios en la bd
                }
                return RedirectToAction("Index"); //Redirigir al index
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}