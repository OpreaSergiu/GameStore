using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineGameStore.Models;

namespace OnlineGameStore.Controllers
{
    [Authorize]
    public class UsersOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            string user_name = User.Identity.GetUserName();

            return View(db.OrderModels.Where(b => b.Email == user_name).ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModels orderModels = db.OrderModels.Find(id);

            if (orderModels == null)
            {
                return HttpNotFound();
            }

            string user_name = User.Identity.GetUserName();

            if (orderModels.Email != user_name)
            {
                return HttpNotFound();
            }

            var resultProductsJoin = db.ProductOrderModels.Where(s => s.CartId == orderModels.Id).ToList();

            var resultProducts = db.ProductsModels.Join(db.ProductOrderModels, s => s.Id, sa => sa.ProductId, (s, sa) => new { products = s, productsJoin = sa }).Where(b => b.productsJoin.CartId == orderModels.Id).Select(ssa => ssa.products);

            var model = new OrderViewModels()
            {
                Order = orderModels,

                ProductsJoin = resultProductsJoin,

                Products = resultProducts
            };

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
