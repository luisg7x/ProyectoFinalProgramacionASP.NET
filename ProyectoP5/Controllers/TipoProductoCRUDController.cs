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
    public class TipoProductoCRUDController : Controller
    {
        //
        // GET: /TipoProductoCRUD/
        //TIPO PRODUCTO CRUD
        public PartialViewResult _AgregarTipoProducto()
        {

            TipoProductoCRUDModel TipoVM = new TipoProductoCRUDModel()
            {
                IdTipoProducto = null,
                NombreProducto = string.Empty,
            };
            return PartialView(TipoVM);
        }

        [HttpPost]
        public ActionResult TipoProductoCRUDAdd(TipoProductoCRUDModel tipo)
        {
            TipoProducto addTipo = new TipoProducto()
            {
                IDTIPOPRODUCTO = tipo.IdTipoProducto.GetValueOrDefault(),
                NOMBRETIPOPRODUCTO = tipo.NombreProducto,
            };

            TipoProductoBLL objBLL = new TipoProductoBLL();

            if (objBLL.AgregarTipoProducto(addTipo) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                return RedirectToAction("Error", "Admin");
            }
        }


        public ActionResult TipoProductoCRUDDelete(int id)
        {
            TipoProductoBLL objBLL = new TipoProductoBLL();

            if (objBLL.BorrarTipoProductoId(id) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult EditarTipoProducto(int id)
        {

            TipoProductoBLL objBLL = new TipoProductoBLL();
            TipoProducto tp = objBLL.BuscarTipoProductoId(id);

            TipoProductoCRUDModel tpVM = new TipoProductoCRUDModel()
            {
                IdTipoProducto = tp.IDTIPOPRODUCTO,
                NombreProducto = tp.NOMBRETIPOPRODUCTO,
            };

            return View(tpVM);
        }

        [HttpPost]
        public ActionResult EditarTipoProducto(TipoProductoCRUDModel tipo)
        {

            TipoProducto tdp = new TipoProducto()
            {
                IDTIPOPRODUCTO = tipo.IdTipoProducto.GetValueOrDefault(),
                NOMBRETIPOPRODUCTO = tipo.NombreProducto,
            };
            TipoProductoBLL objBLL = new TipoProductoBLL();

            if (objBLL.EditarTipoProducto(tdp) == true)
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
        public PartialViewResult _TablaTipoProducto()
        {
            TipoProductoBLL objTipoBLL = new TipoProductoBLL();
            List<TipoProducto> lista = objTipoBLL.ListarTipoProducto();
            List<TipoProductoCRUDModel> tipos = new List<TipoProductoCRUDModel>();
            TipoProductoCRUDModel TipoVM;

            foreach (TipoProducto lst in lista)
            {
                TipoVM = new TipoProductoCRUDModel()
                {
                    IdTipoProducto = lst.IDTIPOPRODUCTO,
                    NombreProducto = lst.NOMBRETIPOPRODUCTO,

                };

                tipos.Add(TipoVM);
            }

            return PartialView(tipos);
        }

	}
}