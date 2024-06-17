using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using System.Dynamic;
using ProyectoP5.Models;
using System.IO;
using ProyectoP5.ManejoRoles;

namespace ProyectoP5.Controllers
{
        [AuthorizationFilter(rol = "admin")]
        public class AdminController : Controller
        {

        
        public ActionResult AdminCRUD()
        {      
            return View();
        }

        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult AdminCalendar()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

	}
}