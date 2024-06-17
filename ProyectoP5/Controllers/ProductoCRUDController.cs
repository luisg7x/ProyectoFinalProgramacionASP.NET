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
    public class ProductoCRUDController : Controller
    {
        //
        // GET: /ProductoCRUD/
        //Producto CRUD

        public PartialViewResult _AgregarProducto()
        {

            ProductoCRUDModel TipoVM = new ProductoCRUDModel()
            {
                IdProducto = string.Empty,
                MarcaProducto = string.Empty,
                ModeloProducto = string.Empty,
                IdTipoProducto = null,
                TipoProducto = getTipoProducto(),
                DescripcionProducto = string.Empty,
                FotoProducto = null,
                PrecioUnitario = null,
                Cantidad = null
            };
            return PartialView(TipoVM);
        }

        [HttpPost]
        public ActionResult ProductoCRUDAdd(ProductoCRUDModel pd, HttpPostedFileBase file)
        {

            if (file != null)
            {
                pd.FotoProducto = new byte[file.ContentLength];
                file.InputStream.Read(pd.FotoProducto, 0, file.ContentLength);

            }

            Producto addProducto = new Producto()
            {


                IDPRODUCTO = pd.IdProducto,
                MARCA = pd.MarcaProducto,
                MODELO = pd.ModeloProducto,
                IDTIPOPRODUCTO = pd.IdTipoProducto.GetValueOrDefault(),
                DESCRIPCION = pd.DescripcionProducto,
                FOTO = pd.FotoProducto,
                PRECIOUNITARIO = pd.PrecioUnitario.GetValueOrDefault(),
                CANTIDADDISPONIBLE = pd.Cantidad.GetValueOrDefault()
            };

            ProductoBLL objBLL = new ProductoBLL();
            if (objBLL.AgregarProducto(addProducto) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                return RedirectToAction("Error", "Admin");
            }

        }

        public ActionResult ProductoCRUDDelete(string id)
        {
            ProductoBLL objBLL = new ProductoBLL();

            if (objBLL.BorrarProductoId(id) == true)
            {
                return RedirectToAction("AdminCRUD", "Admin");
            }
            else
            {
                TempData["error"] = "error de conexión, error de llaves foraneas";
                return RedirectToAction("Error", "Admin");
            }
        }

        public ActionResult EditarProducto(string id)
        {

            ProductoBLL objBLL = new ProductoBLL();
            TipoProductoBLL objBLLTp = new TipoProductoBLL();
            Producto producto = objBLL.BuscarProductoId(id);

            ProductoCRUDModel productoVM = new ProductoCRUDModel()
            {
                IdProducto = producto.IDPRODUCTO,
                MarcaProducto = producto.MARCA,
                ModeloProducto = producto.MODELO,
                IdTipoProducto = producto.IDTIPOPRODUCTO,
                NTipoProducto = objBLLTp.BuscarTipoProductoId(producto.IDTIPOPRODUCTO).NOMBRETIPOPRODUCTO,
                TipoProducto = getTipoProducto(),
                DescripcionProducto = producto.DESCRIPCION,
                FotoProducto = producto.FOTO,
                PrecioUnitario = producto.PRECIOUNITARIO,
                Cantidad = producto.CANTIDADDISPONIBLE
            };

            return View(productoVM);
        }

        [HttpPost]
        public ActionResult EditarProducto(ProductoCRUDModel productoVM, HttpPostedFileBase file)
        {

            ProductoBLL objBLL = new ProductoBLL();
            if (file != null)
            {
                productoVM.FotoProducto = new byte[file.ContentLength];
                file.InputStream.Read(productoVM.FotoProducto, 0, file.ContentLength);

                Producto EditProducto = new Producto()
                {
                    IDPRODUCTO = productoVM.IdProducto,
                    MARCA = productoVM.MarcaProducto,
                    MODELO = productoVM.ModeloProducto,
                    IDTIPOPRODUCTO = productoVM.IdTipoProducto.GetValueOrDefault(),
                    DESCRIPCION = productoVM.DescripcionProducto,
                    FOTO = productoVM.FotoProducto,
                    PRECIOUNITARIO = productoVM.PrecioUnitario.GetValueOrDefault(),
                    CANTIDADDISPONIBLE = productoVM.Cantidad.GetValueOrDefault()
                };

                if (objBLL.EditarProducto(EditProducto) == true)
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

                Producto EditProducto = new Producto()
                {
                    IDPRODUCTO = productoVM.IdProducto,
                    MARCA = productoVM.MarcaProducto,
                    MODELO = productoVM.ModeloProducto,
                    IDTIPOPRODUCTO = productoVM.IdTipoProducto.GetValueOrDefault(),
                    DESCRIPCION = productoVM.DescripcionProducto,
                    FOTO = objBLL.BuscarProductoId(productoVM.IdProducto).FOTO,
                    PRECIOUNITARIO = productoVM.PrecioUnitario.GetValueOrDefault(),
                    CANTIDADDISPONIBLE = productoVM.Cantidad.GetValueOrDefault()
                };


                if (objBLL.EditarProducto(EditProducto) == true)
                {
                    return RedirectToAction("AdminCRUD", "Admin");
                }
                else
                {
                    TempData["error"] = "error de conexión, error de llaves foraneas/primarias/unicas";
                    return RedirectToAction("Error", "Admin");
                }
            }




        }


        public PartialViewResult _TablaProducto()
        {
            ProductoBLL objBLL = new ProductoBLL();
            TipoProductoBLL objBLLTP = new TipoProductoBLL();
            List<Producto> lista = objBLL.ListarProductos();
            List<ProductoCRUDModel> products = new List<ProductoCRUDModel>();
            ProductoCRUDModel PdVM;

            foreach (Producto lst in lista)
            {
                PdVM = new ProductoCRUDModel()
                {
                    IdProducto = lst.IDPRODUCTO,
                    MarcaProducto = lst.MARCA,
                    ModeloProducto = lst.MODELO,
                    NTipoProducto = objBLLTP.BuscarTipoProductoId(lst.IDTIPOPRODUCTO).NOMBRETIPOPRODUCTO,
                    DescripcionProducto = lst.DESCRIPCION,
                    FotoProducto = lst.FOTO,
                    PrecioUnitario = lst.PRECIOUNITARIO,
                    Cantidad = lst.CANTIDADDISPONIBLE
                };

                products.Add(PdVM);
            }

            return PartialView(products);
        }

        public IEnumerable<SelectListItem> getTipoProducto()
        {
            TipoProductoBLL objBLL = new TipoProductoBLL();
            List<TipoProducto> lista = objBLL.ListarTipoProducto();
            List<SelectListItem> ids = new List<SelectListItem>();


            foreach (TipoProducto sd in lista)
            {
                var item = new SelectListItem { Text = sd.NOMBRETIPOPRODUCTO, Value = (sd.IDTIPOPRODUCTO).ToString() };
                ids.Add(item);
            }
            return ids;

        }

	}
}