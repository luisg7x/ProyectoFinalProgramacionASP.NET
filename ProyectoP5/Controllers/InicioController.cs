using ProyectoP5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using BLL;
using DAL;
using System.Dynamic;
using ProyectoP5.ManejoRoles;
using System.Net.Mail;
using System.Net;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Reflection;

namespace ProyectoP5.Controllers
{
    public class InicioController : Controller
    {
        //
        // GET: /Proyecto/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            EmpresaBLL empresaBLL = new EmpresaBLL();
            Empresa empresa = empresaBLL.BuscarPrimeraEspresaId();
            EmpresaCRUDModel company = new EmpresaCRUDModel()
            {
                IdEmpresa = empresa.IDEMPRESA,
                NombreEmpresa = empresa.NOMBRE,
                CorreoEmpresa = empresa.CORREO,
                DireccionEmpresa = empresa.DIRECCION,
                TelefonoEmpresa = empresa.TELEFONO
            };
            return View(company);
        }

        [AuthorizationFilter(rol = "cliente")]
        [HttpPost]
        public ActionResult Contact(string asunto, string mensaje)
        {
            ClienteBLL clientebll = new ClienteBLL();
            EmpresaBLL empresaBLL = new EmpresaBLL();
            sendMail(empresaBLL.BuscarPrimeraEspresaId().CORREO, asunto, mensaje, clientebll.BuscarClienteId(Session["id"].ToString()).CORREO);

            return View();
        }


        public ActionResult About()
        {
 
            return View();
        }

        public PartialViewResult Footer()
        {
            EmpresaBLL empresaBLL = new EmpresaBLL();
            Empresa empresa = empresaBLL.BuscarPrimeraEspresaId();

            if (empresa != null)
            {
                EmpresaCRUDModel company = new EmpresaCRUDModel()
                {
                    IdEmpresa = empresa.IDEMPRESA,
                    NombreEmpresa = empresa.NOMBRE,
                    CorreoEmpresa = empresa.CORREO,
                    DireccionEmpresa = empresa.DIRECCION,
                    TelefonoEmpresa = empresa.TELEFONO
                };

                return PartialView(company);
            }

            return PartialView();
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string contrasena)
        {
            ClienteBLL clientebll = new ClienteBLL();
            AdminBLL adminbll = new AdminBLL();
            var cliente = clientebll.LoginCliente(usuario, Cryptor.Encrypt(contrasena, true));
            var admin = adminbll.LoginAdmin(usuario, Cryptor.Encrypt(contrasena, true));
            //var admin = adminbll.LoginAdmin(usuario, contrasena);
            if (cliente != null)
            {
                //CreateActionInvoker variable
                Session["usuario"] = cliente.USUARIO;
                Session["id"] = cliente.IDCLIENTE;
                Session["rol"] = "cliente";
                return RedirectToAction("LoggedIn");
            }
            else if (admin != null)
            {
                Session["usuario"] = admin.USUARIO;
                Session["id"] = admin.IDADMIN;
                Session["rol"] = "admin";
                return RedirectToAction("LoggedIn");
            }

            ViewData["login"] = "Datos incorrectos";
            return View();
            
        }

        public ActionResult LoggedIn()
        {
            if (Session["Usuario"] != null)
            {
                if (Session["rol"] == "admin")
                {
                    return RedirectToAction("AdminHome", "Admin");
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult Logout()
        {
            Session.RemoveAll();

            return View("Login");
        }



        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(ClienteCRUDModel clienteMV)
        {
            if (CorreoExiste(clienteMV.Correo, clienteMV.IdCliente) == false)
            {
                if (UsuarioExiste(clienteMV.Usuario, clienteMV.IdCliente) == false)
                {
                    Cliente addCliente = new Cliente()
                    {
                        IDCLIENTE = clienteMV.IdCliente,
                        NOMBRE = clienteMV.NombreCliente,
                        APELLIDOS = clienteMV.ApellidoCliente,
                        SEXO = clienteMV.Sexo,
                        NUMTARJETA = Cryptor.Encrypt(clienteMV.NumTarjeta, true),
                        CORREO = clienteMV.Correo,
                        USUARIO = clienteMV.Usuario,
                        CONTRASEÑA = Cryptor.Encrypt(clienteMV.Password, true)

                    };

                    ClienteBLL objBLL = new ClienteBLL();

                    if (objBLL.AgregarCliente(addCliente) == true)
                    {
                        return View("Login");
                    }
                    else
                    {

                        ViewData["error"] = " error de conexión, datos incorrectos";
                        return View("Error", ViewBag.Aviso);
                    }
                }
                else
                {
                    ViewData["error"] = "el usuario ya existe";
                    return View();
                }
            }
            else
            {
                ViewData["error"] = "el correo ya existe";
                return View();
            }
      
        }

        public ActionResult Producto(int id)
        {
            ProductoBLL objBLL = new ProductoBLL();
            TipoProductoBLL objBLLTp = new TipoProductoBLL();
            List<Producto> lista = objBLL.BuscarListaPorTipoProductoId(id);
            List<ProductoCRUDModel> productos = new List<ProductoCRUDModel>();
            ProductoCRUDModel productoVM;

            foreach (Producto lst in lista)
            {
                productoVM = new ProductoCRUDModel()
                {
                    IdProducto = lst.IDPRODUCTO,
                    MarcaProducto = lst.MARCA,
                    ModeloProducto = lst.MODELO,
                    IdTipoProducto  = lst.IDTIPOPRODUCTO,
                    NTipoProducto = objBLLTp.BuscarTipoProductoId(lst.IDTIPOPRODUCTO).NOMBRETIPOPRODUCTO,
                    DescripcionProducto = lst.DESCRIPCION,
                    FotoProducto = lst.FOTO,
                    PrecioUnitario = lst.PRECIOUNITARIO,
                    Cantidad = lst.CANTIDADDISPONIBLE
                };

                productos.Add(productoVM);
            }

            return View(productos);
        }

        public ActionResult DetallesProducto(string id)
        {
            ProductoBLL objBLL = new ProductoBLL();
            Producto producto = objBLL.BuscarProductoId(id);

            ProductoCRUDModel productoVM = new ProductoCRUDModel()
            {
                IdProducto = producto.IDPRODUCTO,
                MarcaProducto = producto.MARCA,
                ModeloProducto = producto.MODELO,
                IdTipoProducto = producto.IDTIPOPRODUCTO,
                DescripcionProducto = producto.DESCRIPCION,
                FotoProducto = producto.FOTO,
                PrecioUnitario = producto.PRECIOUNITARIO,
                Cantidad = producto.CANTIDADDISPONIBLE

            };
            return View(productoVM);
        }

        [AuthorizationFilter(rol = "cliente")]
        public ActionResult EditarPerfil()
        {

            ClienteBLL objBLL = new ClienteBLL();
            Cliente cliente = objBLL.BuscarClienteId(Session["id"].ToString());

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
        public ActionResult EditarPerfil(ClienteCRUDModel clVM)
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
                        return View("Index");
                    }
                    else
                    {
                        ViewData["error"] = " error de conexión, datos incorrectos";
                        return View("Error");
                    }
                }
                else
                {
                    ViewData["error"] = "el usuario ya existe";
                    return View();
                }
            }
            else
            {
                ViewData["error"] = "el correo ya existe";
                return View();
            }


        }


        public ActionResult Carrito()
        {

            return View();
        }


        public ActionResult addItemCarrito(string id)
        {
            ProductoBLL objBLL = new ProductoBLL();
            Producto producto = objBLL.BuscarProductoId(id);
            if (Session["carro"] == null)
            {
                List<CarritoModel> carro = new List<CarritoModel>();
                carro.Add(new CarritoModel(producto, 1));
                Session["carro"] = carro;
            }
            else
            {
                List<CarritoModel> carro = (List<CarritoModel>)Session["carro"];
                int? IndexExistente = getIndex(id);
                if (IndexExistente == null)
                {
                    carro.Add(new CarritoModel(producto, 1));
                }
                else
                {
                    carro[IndexExistente.GetValueOrDefault()].Cantidad++;
                }
                Session["carro"] = carro;
            }


            return RedirectToAction("Carrito");
        }

        private int? getIndex(string id)
        {
            List<CarritoModel> carro = (List<CarritoModel>)Session["carro"];
            for (int i = 0; i < carro.Count; i++)
            {
                if (carro[i].Producto.IDPRODUCTO == id)
                {
                    return i;
                }
            }
            return null;
        }

        public ActionResult EliminarItemCarrito(string id)
        {
            List<CarritoModel> carro = (List<CarritoModel>)Session["carro"];
            carro.RemoveAt(getIndex(id).GetValueOrDefault());
            return RedirectToAction("Carrito");
        }

        public ActionResult MasItem(string id)
        {
            List<CarritoModel> carro = (List<CarritoModel>)Session["carro"];
            int? IndexExistente = getIndex(id);
            carro[IndexExistente.GetValueOrDefault()].Cantidad++;
            return RedirectToAction("Carrito");
        }
        public ActionResult MenosItem(string id)
        {
            List<CarritoModel> carro = (List<CarritoModel>)Session["carro"];
            int? IndexExistente = getIndex(id);
         
            if(carro[IndexExistente.GetValueOrDefault()].Cantidad == 1){
                EliminarItemCarrito(id);
            }
            else
            {
                carro[IndexExistente.GetValueOrDefault()].Cantidad--;
            }
            return RedirectToAction("Carrito");
        }


        //Mejorar esto
        public ActionResult Facturar()
        {
            FacturaBLL facturaBLL = new FacturaBLL();
            DetalleFacturaBLL detalleBLL = new DetalleFacturaBLL();
            EmpresaBLL empresaBLL = new EmpresaBLL();
            ProductoBLL productoBLL = new ProductoBLL();
            ClienteBLL clienteBLL = new ClienteBLL();

            List<CarritoModel> carro = (List<CarritoModel>)Session["carro"];
            List<DetalleFactura> listaDetalle = new List<DetalleFactura>();

            if (Session["rol"] == "cliente")
            {
             
                if (carro != null && carro.Count > 0)
                {
                    foreach (var item in carro)
                    {
                        if (productoBLL.BuscarProductoId(item.Producto.IDPRODUCTO).CANTIDADDISPONIBLE <= 0)
                        {
                            ViewData["error"] = "stock insufciente en el ultimo segundo";
                            return View("Error", ViewBag.Aviso);
                        }
                    }

                    decimal total = carro.Sum(x => x.Producto.PRECIOUNITARIO * x.Cantidad);
                    Empresa empresa = empresaBLL.BuscarPrimeraEspresaId();

                    Nullable<System.DateTime> FECHA = DateTime.Now;

                    Factura addFactura = new Factura()
                    {
                        //mejorar id empresa
                        IDEMPRESA = empresa.IDEMPRESA,
                        FECHA = FECHA,
                        PRECIOTOTAL = total,
                        IDCLIENTE = Session["id"].ToString()
                    };

                    if (facturaBLL.AgregarFacturas(addFactura) == false)
                    {
                        ViewData["error"] = "error de conexión";
                        return View("Error", ViewBag.Aviso);
                    }

                    int NumFactura = facturaBLL.BuscarUltimoId().NUMFACTURA;

                    DetalleFactura addDetalle = new DetalleFactura();
                    foreach (var item in carro)
                    {

                        addDetalle = new DetalleFactura()
                        {

                            NUMFACTURA = NumFactura,
                            IDPRODUCTO = item.Producto.IDPRODUCTO,
                            CANTIDADPRODUCTO = item.Cantidad,
                            PRECIOPARCIAL = item.Producto.PRECIOUNITARIO
                        };
                        listaDetalle.Add(addDetalle);
                    }
                    detalleBLL.MultipleAgregarDetalleFactura(listaDetalle);
                    productoBLL.MultipleActualizarStock(listaDetalle);


                    FacturarClienteModel facturarMV = new FacturarClienteModel()
                    {
                       FECHA = FECHA,
                       NUMFACTURA = NumFactura,
                       IDCLIENTE = Session["id"].ToString(),
                       PRECIOTOTAL = total,
                       NombreEmpresa = empresa.NOMBRE,
                       TelefonoEmpresa = empresa.TELEFONO,
                       CorreoEmpresa = empresa.CORREO,
                       DireccionEmpresa = empresa.DIRECCION
                    };

                    EnviarFacturaCorreo(FECHA.ToString(), total, empresa, NumFactura, Session["id"].ToString(), clienteBLL.BuscarClienteId(Session["id"].ToString()).CORREO.ToString());

                    return View("Facturar", facturarMV);
                }
                return RedirectToAction("Carrito");
            }
            else if (Session["rol"] == "admin")
            {
                return RedirectToAction("Carrito"); 
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Error()
        {
            return View();
        }

        [AuthorizationFilter(rol = "cliente")]
        public ActionResult HistorialCompras()
        {
            FacturaBLL facturabll = new FacturaBLL();
            DetalleFacturaBLL detallebll = new DetalleFacturaBLL();
            ProductoBLL productobll = new ProductoBLL();
      

            List<HistorialComprasModel> inv = new List<HistorialComprasModel>();

            List<Factura> factura = facturabll.BuscarClienteFacturaId(Session["id"].ToString());

             HistorialComprasModel historialVM;

            foreach(Factura fact in factura)
            {
                int numfactura = fact.NUMFACTURA;
                List<DetalleFactura> detalle = detallebll.BuscarDetalleFacturaNumFactura(fact.NUMFACTURA);
                foreach(DetalleFactura deta in detalle)
                {
                    Producto producto = productobll.BuscarProductoId(deta.IDPRODUCTO);
                      historialVM = new HistorialComprasModel()
                        {
                            PRODUCTO = producto.MARCA + " " + producto.MODELO,
                            MARCA = producto.MARCA,
                            MODELO = producto.MODELO,
                            NUMFACTURA = fact.NUMFACTURA,
                            CANTIDADPRODUCTO = deta.CANTIDADPRODUCTO,
                            PRECIOPARCIAL = deta.PRECIOPARCIAL,
                            FECHA = fact.FECHA,
                            PRECIOTOTAL = fact.PRECIOTOTAL,
                            FOTO = producto.FOTO
                        };
                    inv.Add(historialVM);
                }
                detalle.Clear();

            }
            return View(inv);
        }

        [AuthorizationFilter(rol = "cliente")]
        public ActionResult FacturaHistorial(int id)
        {
            FacturaBLL facturabll = new FacturaBLL();
            DetalleFacturaBLL detallebll = new DetalleFacturaBLL();
            ProductoBLL productobll = new ProductoBLL();
            EmpresaBLL empresabll = new EmpresaBLL();

      
            Empresa empresa = empresabll.BuscarPrimeraEspresaId();
            List<HistorialComprasModel> inv = new List<HistorialComprasModel>();

           Factura factura = facturabll.BuscarFacturaId(id);

            HistorialComprasModel historialVM;   
                List<DetalleFactura> detalle = detallebll.BuscarDetalleFacturaNumFactura(factura.NUMFACTURA);
                foreach (DetalleFactura deta in detalle)
                {
                    Producto producto = productobll.BuscarProductoId(deta.IDPRODUCTO);
                    historialVM = new HistorialComprasModel()
                    {
                        IDCLIENTE = Session["id"].ToString(),
                        PRODUCTO = producto.MARCA + " " + producto.MODELO,
                        MARCA = producto.MARCA,
                        MODELO = producto.MODELO,
                        NUMFACTURA = factura.NUMFACTURA,
                        CANTIDADPRODUCTO = deta.CANTIDADPRODUCTO,
                        PRECIOPARCIAL = deta.PRECIOPARCIAL,
                        FECHA = factura.FECHA,
                        PRECIOTOTAL = factura.PRECIOTOTAL,
                        FOTO = producto.FOTO,
                        IDEMPRESA = empresa.IDEMPRESA,
                        NOMBREEMPRESA = empresa.NOMBRE,
                        TELEFONOEMPRESA = empresa.TELEFONO,
                        DIRECCIONEMPRESA = empresa.DIRECCION,
                        CORREOEMPRESA = empresa.CORREO
                    };
                    inv.Add(historialVM);
                }
                detalle.Clear();
            
          
            return View(inv);
        }

        [NonAction]
        public void sendMail(string mailEmpresa, string asunto, string mensaje, string mailUsuario)
        {
            var fromEmail = new MailAddress("LuisG101010x@gmail.com", "GPC - Mensaje");
            var toMail = new MailAddress(mailEmpresa);
            var fromEmailPassword = "luisg710";
            string subject = "Mensaje";

            string body = "<br> <h2> Asunto: </h2> <p style='font:16px'>" + asunto + "</p> <br>" +
                 "<h2> Mensaje: </h2> <p style='font:16px'>" + mensaje + "</p> <br>" +
                 "<h2> Correo Usuario: </h2> <p style='font:16px'>" + mailUsuario + "</p> <br>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)            
            };

            using (var message = new MailMessage(fromEmail, toMail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
                
            })
                smtp.Send(message);

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

          
        private void EnviarFacturaCorreo(string date, decimal total, Empresa empresa, int numfactura, string idCliente, string mail)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'> <h2 style='color:#000 font:36px Freestyle Script'>Gaming PC</h2> </td></tr>");
                    sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.Append("<tr><td><b>Número de factura:</b> "+ numfactura + "</td><td><b>Fecha: </b>" + date + " </td></tr>");
                    sb.Append("<tr><td><b>Id Cliente:</b> " + idCliente);
                    sb.Append("<tr><td></td></tr>");
                    sb.Append("</table>");

                     sb.Append("<table border = '1'>");

                    sb.Append("<tr>");
                        sb.Append("<th>PRODUCTO</th>");
                        sb.Append("<th>PRECIO UNITARIO</th>");
                        sb.Append("<th>CANTIDAD</th>");
                        sb.Append("<th>TOTAL</th>");
                    sb.Append("</tr>");

                    foreach (var item in Session["carro"] as List<ProyectoP5.Models.CarritoModel>)
                            {
                                sb.Append("<tr>");

                                sb.Append("<td>");
                                sb.Append("<h5>");
                                sb.Append(item.Producto.MARCA + " " + item.Producto.MODELO);
                                sb.Append("</h5>");
                                sb.Append("</td>");

                                sb.Append("<td>");
                                sb.Append("$" + item.Producto.PRECIOUNITARIO);
                                sb.Append("</td>");

                                sb.Append("<td>");
                                sb.Append(item.Cantidad);
                                sb.Append("</td>");

                                sb.Append("<td>");
                                sb.Append("$" + (item.Cantidad * item.Producto.PRECIOUNITARIO));
                                sb.Append("</td>");

                                sb.Append("</tr>");
                            }

                    sb.Append("<tr>");

                    sb.Append("<td colspan = '3'>");
                    sb.Append("</td>");

                    sb.Append("<td>");
                    sb.Append("$" + total);
                    sb.Append("</td>");

                    sb.Append("</tr>");


                    sb.Append("</table>");


                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Telefono Empresa:</b> " + empresa.TELEFONO +  "</td></tr>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Correo Empresa:</b> " + empresa.CORREO + "</td></tr>");
                    sb.Append("</table>");

                    StringReader sr = new StringReader(sb.ToString());

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();

                        MailMessage mm = new MailMessage("LuisG101010x@gmail.com", mail);
                        mm.Subject = "Factura de Compra: " + numfactura;
                        mm.Body = "Gracias por su compra!";
                        mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "GPCFacturaNum"+ numfactura + ".pdf"));
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        NetworkCred.UserName = "LuisG101010x@gmail.com";
                        NetworkCred.Password = "luisg710";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
            }
        }





    }
}