using BLL;
using DAL;
using ProyectoP5.ManejoRoles;
using ProyectoP5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoP5.Controllers
{
    [AuthorizationFilter(rol = "admin")]
    public class AdminCRUDController : Controller
    {
        //TABLE ADMIN CRUD
        public PartialViewResult _AgregarAdmin()
        {
            AdminCRUDModel AdminVM = new AdminCRUDModel()
            {
                IdAdmin = string.Empty,
                NombreAdmin = string.Empty,
                ApellidoAdmin = string.Empty,
                Usuario = string.Empty,
                Contraseña = string.Empty

            };
            return PartialView(AdminVM);
        }


        [HttpPost]
        public ActionResult AdminCRUDAdd(AdminCRUDModel admin)
        {
            if (UsuarioExiste(admin.Usuario, admin.IdAdmin) == false)
            {
                Admin addAdmin = new Admin()
                {
                    IDADMIN = admin.IdAdmin,
                    NOMBRE = admin.NombreAdmin,
                    APELLIDOS = admin.ApellidoAdmin,
                    USUARIO = admin.Usuario,
                    CONTRASEÑA = Cryptor.Encrypt(admin.Contraseña, true)
                };

                AdminBLL objAdminBLL = new AdminBLL();

                if (objAdminBLL.AgregarAdmin(addAdmin) == true)
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

        public ActionResult EditarAdmin(string id)
        {

            AdminBLL objAdminBLL = new AdminBLL();
            Admin adm = objAdminBLL.BuscarAdminId(id);

            AdminCRUDModel adminVM = new AdminCRUDModel()
            {
                IdAdmin = adm.IDADMIN,
                NombreAdmin = adm.NOMBRE,
                ApellidoAdmin = adm.APELLIDOS,
                Usuario = adm.USUARIO,
                Contraseña = Cryptor.Decrypt(adm.CONTRASEÑA, true)

            };

            return View(adminVM);
        }

        [HttpPost]
        public ActionResult EditarAdmin(AdminCRUDModel admVM)
        {
              if (UsuarioExiste(admVM.Usuario, admVM.IdAdmin) == false)
                {
                      Admin adm = new Admin()
                      {
                          IDADMIN = admVM.IdAdmin,
                          NOMBRE = admVM.NombreAdmin,
                          APELLIDOS = admVM.ApellidoAdmin,
                          USUARIO = admVM.Usuario,
                          CONTRASEÑA = Cryptor.Encrypt(admVM.Contraseña, true)
                      };
                      AdminBLL objAdminBLL = new AdminBLL();


                      if (objAdminBLL.EditarAdmin(adm) == true)
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

        public ActionResult AdminCRUDDelete(string id)
        {
            AdminBLL objAdminBLL = new AdminBLL();

            if (objAdminBLL.BorrarAdminId(id) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas";
                return RedirectToAction("Error", "Admin");
            }
        }

        //render
        public PartialViewResult _TablaAdmin()
        {
            AdminBLL objAdminBLL = new AdminBLL();
            List<Admin> lista = objAdminBLL.ListarAdmins();
            List<AdminCRUDModel> admins = new List<AdminCRUDModel>();
            AdminCRUDModel AdminVM;

            foreach (Admin lst in lista)
            {
                AdminVM = new AdminCRUDModel()
                {
                    IdAdmin = lst.IDADMIN,
                    NombreAdmin = lst.NOMBRE,
                    ApellidoAdmin = lst.APELLIDOS,
                    Usuario = lst.USUARIO,
                    Contraseña = lst.CONTRASEÑA

                };

                admins.Add(AdminVM);
            }

            return PartialView(admins);
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