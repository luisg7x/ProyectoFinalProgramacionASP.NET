using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using ProyectoP5.Models;
using ProyectoP5.ManejoRoles;

namespace ProyectoP5.Controllers
{
    [AuthorizationFilter(rol = "admin")]
    public class ClienteCRUDController : Controller
    {
        //
        // GET: /ClienteCRUD/

        public PartialViewResult _AgregarCliente()
        {

            ClienteCRUDModel ClienteVM = new ClienteCRUDModel()
            {
                IdCliente = string.Empty,
                NombreCliente = string.Empty,
                ApellidoCliente = string.Empty,
                Sexo = string.Empty,
                NumTarjeta = string.Empty,
                Correo = string.Empty,
                Usuario = string.Empty,
                Password = string.Empty,
            };
            return PartialView(ClienteVM);
        }

        //Cliente CRUD
        [HttpPost]
        public ActionResult ClienteCRUDAdd(ClienteCRUDModel cl)
        {
            if (CorreoExiste(cl.Correo, cl.IdCliente) == false)
            {
                if (UsuarioExiste(cl.Usuario, cl.IdCliente) == false)
                {
                   Cliente addCliente = new Cliente()
                   {
                       IDCLIENTE = cl.IdCliente,
                       NOMBRE = cl.NombreCliente,
                       APELLIDOS = cl.ApellidoCliente,
                       SEXO = cl.Sexo,
                       NUMTARJETA = Cryptor.Encrypt(cl.NumTarjeta, true),
                       CORREO = cl.Correo,
                       USUARIO = cl.Usuario,
                       CONTRASEÑA = Cryptor.Encrypt(cl.Password, true)

                   };

                   ClienteBLL objBLL = new ClienteBLL();

                   if (objBLL.AgregarCliente(addCliente) == true)
                   {
                       return RedirectToAction("AdminCRUD", "Admin");
                   }
                   else
                   {
                       TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                       return RedirectToAction("Error", "Admin");
                   }
               }
               else
               {

                   TempData["error"] = "error de conexión, el usuario ya existe";
                   return RedirectToAction("Error", "Admin");
               }
           }
            else
             {

                 TempData["error"] = "error de conexión, el correo ya existe";
                  return RedirectToAction("Error", "Admin");
             }
        }

        public ActionResult ClienteCRUDDelete(string id)
        {
            ClienteBLL objBLL = new ClienteBLL();

            if (objBLL.BorrarClienteId(id) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult EditarCliente(string id)
        {

            ClienteBLL objBLL = new ClienteBLL();
            Cliente cliente = objBLL.BuscarClienteId(id);

            ClienteCRUDModel clVM = new ClienteCRUDModel()
            {
                IdCliente = cliente.IDCLIENTE,
                NombreCliente = cliente.NOMBRE,
                ApellidoCliente = cliente.APELLIDOS,
                Sexo = cliente.SEXO,
                NumTarjeta = Cryptor.Decrypt(cliente.NUMTARJETA, true),
                Correo = cliente.CORREO,
                Usuario = cliente.USUARIO,
                Password = Cryptor.Decrypt(cliente.CONTRASEÑA, true)
            };

            return View(clVM);
        }

        [HttpPost]
        public ActionResult EditarCliente(ClienteCRUDModel clVM)
        {
            if (CorreoExiste(clVM.Correo, clVM.IdCliente) == false)
            {
                if (UsuarioExiste(clVM.Usuario, clVM.IdCliente) == false)
                {
                    Cliente addCliente = new Cliente()
                    {
                        IDCLIENTE = clVM.IdCliente,
                        NOMBRE = clVM.NombreCliente,
                        APELLIDOS = clVM.ApellidoCliente,
                        SEXO = clVM.Sexo,
                        NUMTARJETA = Cryptor.Encrypt(clVM.NumTarjeta, true),
                        CORREO = clVM.Correo,
                        USUARIO = clVM.Usuario,
                        CONTRASEÑA = Cryptor.Encrypt(clVM.Password, true)
                    };
                    ClienteBLL objBLL = new ClienteBLL();

                    if (objBLL.EditarCliente(addCliente) == true)
                    {
                        return RedirectToAction("AdminCRUD", "Admin");
                    }
                    else
                    {
                        TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                        return RedirectToAction("Error", "Admin");
                    }
                }
                else
                {
                    ViewData["error"] = "Error: El usuario ya existe";
                    return View();
                }
            }
            else
             {
                    ViewData["error"] = "Error: El correo ya existe";
                    return View();
              }

        }

        //render
        public PartialViewResult _TablaCliente()
        {
            ClienteBLL objBLL = new ClienteBLL();
            List<Cliente> lista = objBLL.ListarClientes();
            List<ClienteCRUDModel> clientes = new List<ClienteCRUDModel>();
            ClienteCRUDModel clienteVM;

            foreach (Cliente lst in lista)
            {
                clienteVM = new ClienteCRUDModel()
                {
                    IdCliente = lst.IDCLIENTE,
                    NombreCliente = lst.NOMBRE,
                    ApellidoCliente = lst.APELLIDOS,
                    Sexo = lst.SEXO,
                    NumTarjeta = lst.NUMTARJETA,
                    Correo = lst.CORREO,
                    Usuario = lst.USUARIO,
                    Password = lst.CONTRASEÑA,
                };

                clientes.Add(clienteVM);
            }

            return PartialView(clientes);
        }


        private bool CorreoExiste(string correo, string id)
        {
            ClienteBLL clientebll = new ClienteBLL();
            Cliente cliente = clientebll.BuscarClienteId(id);
            if (clientebll.BuscarClienteCorreo(correo) == null)
            {
                return false;
            }
            else if (cliente != null)
            {
                if (cliente.CORREO.Equals(correo))
                {
                    return false;
                }
            }
            return true;
        }

        private bool UsuarioExiste(string usuario, string id)
        {
            AdminBLL adminbll = new AdminBLL();
            ClienteBLL clientebll = new ClienteBLL();
            
            Admin admin = adminbll.BuscarAdminId(id);
            Cliente cliente = clientebll.BuscarClienteId(id);

            if (clientebll.BuscarClienteUsuario(usuario) == null && adminbll.BuscarAdminUsuario(usuario) == null)
            {
                return false;
            }
            else if (admin != null)
                {
                    if (admin.USUARIO.Equals(usuario))
                    {
                        return false;
                    }
                }
            else if (cliente != null)
                {
                    if (cliente.USUARIO.Equals(usuario))
                    {
                        return false;
                    }
                }
            
            return true;
        }
	}
}