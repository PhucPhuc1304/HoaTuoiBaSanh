using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoaTuoiBaSanh.FormatHelper
{
    public static class FormatHelper
    {
        public static string FormatPriceVND(double price)
        {
            double giaLe = (double)price;
            string giaLeFomat = giaLe.ToString("N0") ;
            return giaLeFomat;
        }  
    }
    }
