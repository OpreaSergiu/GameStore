using Microsoft.AspNet.Identity;
using OnlineGameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineGameStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [ChildActionOnly]
        public ActionResult _ShopingCart()
        {
            string user_name = User.Identity.GetUserName();

            var model = db.CartModels.SingleOrDefault(b => b.Email == user_name);

            return PartialView(model);
        }

        public ActionResult Index()
        {
            var model = new GamesViewModels()
            {
                Products = db.ProductsModels.OrderByDescending(s => s.AddDate).ToList().Take(6)
            };

            return View(model);
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

        public ActionResult AddToCart(int id)
        {
            string user_name = User.Identity.GetUserName();

            var result = db.CartModels.SingleOrDefault(b => b.Email == user_name);

            if (result == null)
            {
                string redirectUrl1 = "/Account/Login";
                return Redirect(redirectUrl1);
            }

            if (result != null)
            {
                var product = db.ProductsModels.Find(id);
                var productToCart = new ProductsChartModels();

                productToCart.CartId = result.Id;
                productToCart.ProductId = product.Id;

                result.TotalAmount = result.TotalAmount + product.CurrentPrice;
                result.NumberOfProducts = result.NumberOfProducts + 1;

                db.ProductsChartModels.Add(productToCart);
                db.SaveChanges();
            }

            string redirectUrl = "/Home/Index";
            return Redirect(redirectUrl);
        }
    }
}