using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace ProyectoP5.Models
{
    public class CarritoModel
    {
        private Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public CarritoModel()
        {

        }
        public CarritoModel(Producto producto, int cantidad)
        {
            this.producto = producto;
            this.cantidad = cantidad;
        }
    }
}