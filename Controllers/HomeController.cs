using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoaTuoiBaSanh.Models;
namespace HoaTuoiBaSanh.Controllers
{
    public class HomeController : Controller
    {
        MyDBContext data = new MyDBContext();
        public ActionResult Index()
        {
            var newProduct = data.HangHoas.OrderBy(x => x.GiaLe).Take(4).ToList();
            var saleProduct = data.HangHoas.OrderByDescending(x => x.DonGia1).Take(4).ToList();
            Product product = new Product();
            product.newProduct = newProduct;
            product.saleProduct = saleProduct;
            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}