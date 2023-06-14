using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoaTuoiBaSanh.Models
{
    public class CartModel
    {
        public HangHoa sanPham { get; set; }
        public int Quantity { get; set; }
        public double Total => (double)(sanPham.GiaLe * Quantity);
        public double Subtotal { get; set; }
    }
}