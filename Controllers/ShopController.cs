using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoaTuoiBaSanh.Models;
using PagedList;

namespace HoaTuoiBaSanh.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        MyDBContext data = new MyDBContext();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, String Loai, string priceFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var products = data.HangHoas.AsQueryable(); // Replace 'db.Products' with your actual entity set
            var categories = data.LoaiHangs.ToList();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category.MaLoai.ToString(),
                    Text = category.TenLoai
                });
            }
            ViewBag.CategoryList = categoryList;

            ViewBag.CurrentFilter = searchString;

            // Lọc danh sách hoa dựa trên searchString
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.TenHang.Contains(searchString));
            }

            if (Loai != null)
            {
                products = products.Where(p => p.MaLoai == Loai);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.TenHang.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(priceFilter))
            {
                switch (priceFilter)
                {
                    case "0-50":
                        products = products.Where(p => p.GiaLe >= 0 && p.GiaLe <= 20000);
                        break;
                    case "50-100":
                        products = products.Where(p => p.GiaLe > 20000 && p.GiaLe <= 100000);
                        break;
                    case "100-150":
                        products = products.Where(p => p.GiaLe > 10000 && p.GiaLe <= 150000);
                        break;
                    case "150-200":
                        products = products.Where(p => p.GiaLe > 150000 && p.GiaLe <= 200000);
                        break;
                    case "200-250":
                        products = products.Where(p => p.GiaLe > 200000 && p.GiaLe <= 250000);
                        break;
                    case "250":
                        products = products.Where(p => p.GiaLe > 250000);
                        break;
                }
            }


            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.TenHang);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.GiaLe);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.GiaLe);
                    break;
                default:
                    products = products.OrderBy(p => p.TenHang);
                    break;
            }
            ViewBag.CategoryList = new SelectList(data.LoaiHangs, "MaLoai", "TenLoai");

          

          

     
            int pageSize = 9; // Number of items to display on each page
            int pageNumber = (page ?? 1);
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult Cart()
        {
            return View((List<CartModel>)Session["cart"]);
        }

        public ActionResult AddToCart(string id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { sanPham = data.HangHoas.Where(p => p.MaHang == id).FirstOrDefault(), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];

                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;

                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel { sanPham = data.HangHoas.Where(p => p.MaHang == id).FirstOrDefault(), Quantity = quantity});
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;


            }
            double totalPrice = CalculateTotalPrice();
            return Json(new { Message = "Thành công", TotalPrice = totalPrice }, JsonRequestBehavior.AllowGet);

        }

        private double CalculateTotalPrice()
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            double totalPrice = 0;
            foreach (CartModel cartItem in cart)
            {
                totalPrice += cartItem.Total;
                cartItem.Subtotal = totalPrice;
            }
            return totalPrice;
        }

        private int isExist(string id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].sanPham.MaHang.Equals(id))
                    return i;
            return -1;
        }
        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(string Id,int quatity)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.sanPham.MaHang == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - quatity;
            double totalPrice = CalculateTotalPrice();
            return Json(new { Message = "Thành công", TotalPrice = totalPrice }, JsonRequestBehavior.AllowGet);
        }
    }
}
