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
    public class EmpresaCRUDController : Controller
    {
        //
        // GET: /EmpresaCRUD/



        public PartialViewResult _AgregarEmpresa()
        {

            EmpresaCRUDModel empresaVM = new EmpresaCRUDModel()
            {
                IdEmpresa = string.Empty,
                NombreEmpresa = string.Empty,
                TelefonoEmpresa = string.Empty,
                CorreoEmpresa = string.Empty,
                DireccionEmpresa = string.Empty,

            };
            return PartialView(empresaVM);
        }

        //Empresa CRUD
        [HttpPost]
        public ActionResult EmpresaCRUDAdd(EmpresaCRUDModel emp)
        {

            Empresa addEmpresa = new Empresa()
            {
                IDEMPRESA = emp.IdEmpresa,
                NOMBRE = emp.NombreEmpresa,
                TELEFONO = emp.TelefonoEmpresa,
                CORREO = emp.CorreoEmpresa,
                DIRECCION = emp.DireccionEmpresa
            };

            EmpresaBLL objBLL = new EmpresaBLL();


            if (objBLL.AgregarEspresa(addEmpresa) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult EmpresaCRUDDelete(string id)
        {
            EmpresaBLL objBLL = new EmpresaBLL();

            if (objBLL.BorrarEspresaId(id) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult EditarEmpresa(string id)
        {

            EmpresaBLL objBLL = new EmpresaBLL();
            Empresa empresa = objBLL.BuscarEspresaId(id);

            EmpresaCRUDModel empVM = new EmpresaCRUDModel()
            {
                IdEmpresa = empresa.IDEMPRESA,
                NombreEmpresa = empresa.NOMBRE,
                TelefonoEmpresa = empresa.TELEFONO,
                CorreoEmpresa = empresa.CORREO,
                DireccionEmpresa = empresa.DIRECCION
            };

            return View(empVM);
        }

        [HttpPost]
        public ActionResult EditarEmpresa(EmpresaCRUDModel empVM)
        {

            Empresa addEmpresa = new Empresa()
            {
                IDEMPRESA = empVM.IdEmpresa,
                NOMBRE = empVM.NombreEmpresa,
                TELEFONO = empVM.TelefonoEmpresa,
                CORREO = empVM.CorreoEmpresa,
                DIRECCION = empVM.DireccionEmpresa
            };
            EmpresaBLL objBLL = new EmpresaBLL();

            if (objBLL.EditarEspresa(addEmpresa) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                return RedirectToAction("Error", "Admin");
            }

        }

        //render
        public PartialViewResult _TablaEmpresa()
        {
            EmpresaBLL objBLL = new EmpresaBLL();
            List<Empresa> lista = objBLL.ListarEspresas();
            List<EmpresaCRUDModel> empresas = new List<EmpresaCRUDModel>();
            EmpresaCRUDModel empresaVM;

            foreach (Empresa lst in lista)
            {
                empresaVM = new EmpresaCRUDModel()
                {
                    IdEmpresa = lst.IDEMPRESA,
                    NombreEmpresa = lst.NOMBRE,
                    TelefonoEmpresa = lst.TELEFONO,
                    CorreoEmpresa = lst.CORREO,
                    DireccionEmpresa = lst.DIRECCION
                };

                empresas.Add(empresaVM);
            }

            return PartialView(empresas);
        }
	}
}