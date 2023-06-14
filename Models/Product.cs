using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoaTuoiBaSanh.Models
{
    public class Product
    {
        public List<HangHoa> hotProduct { get; set; }
        public List<HangHoa> saleProduct { get; set; }
        public List<HangHoa> newProduct { get; set; }

    }
}