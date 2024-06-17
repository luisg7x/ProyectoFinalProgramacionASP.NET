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
    public class FacturaCRUDController : Controller
    {
        //
        // GET: /FacturaCRUD/
        //Factura CRUD

        public PartialViewResult _AgregarFactura()
        {
            FacturaCRUDModel facturaVM = new FacturaCRUDModel()
            {
                NUMFACTURA = null,
                IDEMPRESA = string.Empty,
                EMPRESALIST = getEmpresa(),
                FECHA = DateTime.Now,
                PRECIOTOTAL = null,
                IDCLIENTE = string.Empty

            };
            return PartialView(facturaVM);
        }


        [HttpPost]
        public ActionResult FacturaCRUDAdd(FacturaCRUDModel ft)
        {

            Factura addFactura = new Factura()
            {
                IDEMPRESA = ft.IDEMPRESA,
                FECHA = ft.FECHA,
                PRECIOTOTAL = ft.PRECIOTOTAL.GetValueOrDefault(),
                IDCLIENTE = ft.IDCLIENTE
            };

            FacturaBLL objBLL = new FacturaBLL();
            if (objBLL.AgregarFacturas(addFactura) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult FacturaCRUDDelete(int id)
        {
            FacturaBLL objBLL = new FacturaBLL();

            if (objBLL.BorrarFacturaId(id) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult EditarFactura(int id)
        {

            FacturaBLL objBLL = new FacturaBLL();
            Factura factura = objBLL.BuscarFacturaId(id);

            FacturaCRUDModel ftVM = new FacturaCRUDModel()
            {
                NUMFACTURA = factura.NUMFACTURA,
                IDEMPRESA = factura.IDEMPRESA,
                EMPRESALIST = getEmpresa(),
                FECHA = factura.FECHA,
                PRECIOTOTAL = factura.PRECIOTOTAL,
                IDCLIENTE = factura.IDCLIENTE
            };

            return View(ftVM);
        }


        [HttpPost]
        public ActionResult EditarFactura(FacturaCRUDModel ftVM)
        {

            Factura editFactura = new Factura()
            {
                NUMFACTURA = ftVM.NUMFACTURA.GetValueOrDefault(),
                IDEMPRESA = ftVM.IDEMPRESA,
                FECHA = ftVM.FECHA,
                PRECIOTOTAL = ftVM.PRECIOTOTAL.GetValueOrDefault(),
                IDCLIENTE = ftVM.IDCLIENTE
            };
            FacturaBLL objBLL = new FacturaBLL();

            if (objBLL.EditarFactura(editFactura) == true)
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
        public PartialViewResult _TablaFactura()
        {
            FacturaBLL objBLL = new FacturaBLL();
            EmpresaBLL objEmpresaBLL = new EmpresaBLL();
            List<Factura> lista = objBLL.ListarFacturas();
            List<FacturaCRUDModel> facturas = new List<FacturaCRUDModel>();
            FacturaCRUDModel facturaVM;

            foreach (Factura lst in lista)
            {
                facturaVM = new FacturaCRUDModel()
                {
                    NUMFACTURA = lst.NUMFACTURA,
                    EMPRESA = objEmpresaBLL.BuscarEspresaId(lst.IDEMPRESA).NOMBRE,
                    FECHA = lst.FECHA,
                    PRECIOTOTAL = lst.PRECIOTOTAL,
                    IDCLIENTE = lst.IDCLIENTE
                };

                facturas.Add(facturaVM);
            }

            return PartialView(facturas);
        }

        public IEnumerable<SelectListItem> getEmpresa()
        {
            EmpresaBLL objBLL = new EmpresaBLL();
            List<Empresa> lista = objBLL.ListarEspresas();
            List<SelectListItem> ids = new List<SelectListItem>();


            foreach (Empresa sd in lista)
            {
                var item = new SelectListItem { Text = sd.NOMBRE, Value = (sd.IDEMPRESA).ToString() };
                ids.Add(item);
            }
            return ids;

        }



	}
}