using Microsoft.AspNet.Identity;
using OnlineGameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineGameStore.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public ActionResult Index()
        {
            var model = new GamesViewModels()
            {
                Products = db.ProductsModels.ToList()
            };

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsModels productsModels = db.ProductsModels.Find(id);
            if (productsModels == null)
            {
                return HttpNotFound();
            }
            return View(productsModels);
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

            string redirectUrl = "/Games";
            return Redirect(redirectUrl);
        }
    }
}