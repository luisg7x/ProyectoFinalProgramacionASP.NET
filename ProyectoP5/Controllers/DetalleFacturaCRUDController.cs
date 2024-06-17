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
    public class DetalleFacturaCRUDController : Controller
    {
        //
        // GET: /DetalleFacturaCRUD/


        public PartialViewResult _AgregarDetalleFactura()
        {
            DetalleFacturaCRUDModel detalleFacturaVM = new DetalleFacturaCRUDModel()
            {
                NUMFACTURA = null,
                IDPRODUCTO = string.Empty,
                PRODUCTOLIST = getProducto(),
                CANTIDADPRODUCTO = null,
                PRECIOPARCIAL = null

            };
            return PartialView(detalleFacturaVM);
        }


        //Detalle Factura CRUD
        [HttpPost]
        public ActionResult DetalleFacturaCRUDAdd(DetalleFacturaCRUDModel ft)
        {

            DetalleFactura addDetalleFactura = new DetalleFactura()
            {
                NUMFACTURA = ft.NUMFACTURA.GetValueOrDefault(),
                IDPRODUCTO = ft.IDPRODUCTO,
                CANTIDADPRODUCTO = ft.CANTIDADPRODUCTO.GetValueOrDefault(),
                PRECIOPARCIAL = ft.PRECIOPARCIAL.GetValueOrDefault()
            };

            DetalleFacturaBLL objBLL = new DetalleFacturaBLL();

            if (objBLL.AgregarDetalleFactura(addDetalleFactura) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult DetalleFacturaCRUDDelete(int id)
        {
            DetalleFacturaBLL objBLL = new DetalleFacturaBLL();
            if (objBLL.BorrarDetalleFacturaId(id) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult EditarDetalleFactura(int id)
        {

            DetalleFacturaBLL objBLL = new DetalleFacturaBLL();
            DetalleFactura detallefactura = objBLL.BuscarDetalleFacturaId(id);

            DetalleFacturaCRUDModel ftVM = new DetalleFacturaCRUDModel()
            {
                IDDETALLEFACTURA = detallefactura.IDDETALLEFACTURA,
                NUMFACTURA = detallefactura.NUMFACTURA,
                IDPRODUCTO = detallefactura.IDPRODUCTO,
                PRODUCTOLIST = getProducto(),
                CANTIDADPRODUCTO = detallefactura.CANTIDADPRODUCTO,
                PRECIOPARCIAL = detallefactura.PRECIOPARCIAL
            };

            return View(ftVM);
        }


        [HttpPost]
        public ActionResult EditarDetalleFactura(DetalleFacturaCRUDModel ftVM)
        {

            DetalleFactura editDetalleFactura = new DetalleFactura()
            {
                IDDETALLEFACTURA = ftVM.IDDETALLEFACTURA.GetValueOrDefault(),
                NUMFACTURA = ftVM.NUMFACTURA.GetValueOrDefault(),
                IDPRODUCTO = ftVM.IDPRODUCTO,
                CANTIDADPRODUCTO = ftVM.CANTIDADPRODUCTO.GetValueOrDefault(),
                PRECIOPARCIAL = ftVM.PRECIOPARCIAL.GetValueOrDefault()
            };
            DetalleFacturaBLL objBLL = new DetalleFacturaBLL();

            if (objBLL.EditarDetalleFactura(editDetalleFactura) == true)
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
        public PartialViewResult _TablaDetalleFactura()
        {
            DetalleFacturaBLL objBLL = new DetalleFacturaBLL();
            ProductoBLL objProductoBLL = new ProductoBLL();
            List<DetalleFactura> lista = objBLL.ListarDetalleFactura();
            List<DetalleFacturaCRUDModel> detalleFacturas = new List<DetalleFacturaCRUDModel>();
            DetalleFacturaCRUDModel detallesFacturaVM;

            foreach (DetalleFactura lst in lista)
            {
                detallesFacturaVM = new DetalleFacturaCRUDModel()
                {
                    IDDETALLEFACTURA = lst.IDDETALLEFACTURA,
                    NUMFACTURA = lst.NUMFACTURA,
                    PRODUCTO = objProductoBLL.BuscarProductoId(lst.IDPRODUCTO).MARCA + " " + objProductoBLL.BuscarProductoId(lst.IDPRODUCTO).MODELO,
                    CANTIDADPRODUCTO = lst.CANTIDADPRODUCTO,
                    PRECIOPARCIAL = lst.PRECIOPARCIAL,
                };

                detalleFacturas.Add(detallesFacturaVM);
            }

            return PartialView(detalleFacturas);
        }


        public IEnumerable<SelectListItem> getProducto()
        {
            ProductoBLL objBLL = new ProductoBLL();
            List<Producto> lista = objBLL.ListarProductos();
            List<SelectListItem> ids = new List<SelectListItem>();


            foreach (Producto sd in lista)
            {
                var item = new SelectListItem { Text = sd.MARCA + " " + sd.MODELO, Value = (sd.IDPRODUCTO).ToString() };
                ids.Add(item);
            }
            return ids;

        }
	}
}