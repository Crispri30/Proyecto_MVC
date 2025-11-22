using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        [HttpPost]
        public ActionResult Nuevo(FormCollection collection) //Se usa action result para devolver una vista
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Proyecto_MVC_BD db = new Proyecto_MVC_BD(); //Conectar a la bd
                    Clientes cliente = new Clientes(); //Crear un objeto del modelo empleados
                    cliente.Nombre = collection["Nombre"];
                    cliente.Documento = collection["Documento"];
                    cliente.Telefono = collection["Telefono"];
                    cliente.Email = collection["Email"];
                    cliente.Direccion = collection["Direccion"];

                    db.Clientes.Add(cliente); //Agregar el objeto a la tabla empleados
                    db.SaveChanges(); //Guardar los cambios en la bd

                    return RedirectToAction("Index"); //Redireccionar a la vista index
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
            Proyecto_MVC_BD db = new Proyecto_MVC_BD(); //Conectar a la bd
            var cliente = db.Clientes.Find(id); //Buscar el empleado por id
            return View(cliente); //Enviar el objeto empleado a la vista
        }
        [HttpPost]
        public ActionResult Editar(Clientes model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Proyecto_MVC_BD db = new Proyecto_MVC_BD(); // conectar a la bd
                    var clientemodificado = db.Clientes.Find(model.ClienteID); //Buscar el cliente en la bd por id
                    clientemodificado.Nombre = model.Nombre;
                    clientemodificado.Documento = model.Documento;
                    clientemodificado.Telefono = model.Telefono;
                    clientemodificado.Email = model.Email;
                    clientemodificado.Direccion = model.Direccion;

                    db.Entry(clientemodificado).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
                
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
                    var cliente = db.Clientes.Find(id); //Buscar el cliente por id
                    db.Clientes.Remove(cliente); //Eliminar el cliente
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