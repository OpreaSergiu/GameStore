using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineGameStore.Models;

namespace OnlineGameStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.OrderModels.ToList());
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

            var resultProductsJoin = db.ProductOrderModels.Where(s => s.CartId == orderModels.Id).ToList();

            var resultProducts = db.ProductsModels.Join(db.ProductOrderModels, s => s.Id, sa => sa.ProductId, (s, sa) => new { products = s, productsJoin = sa }).Where(b=> b.productsJoin.CartId == orderModels.Id).Select(ssa => ssa.products);

            var model = new OrderViewModels()
            {
                Order = orderModels,

                ProductsJoin = resultProductsJoin,

                Products = resultProducts
            };

            return View(model);
        }

        public ActionResult DeleteFromOrder(int id, int OredrId)
        {
            OrderModels orderModels = db.OrderModels.Find(OredrId);

            if (orderModels != null)
            {
                var product = db.ProductsModels.Find(id);
                var productToCart = db.ProductOrderModels.Where(s => s.CartId == orderModels.Id).Where(b => b.ProductId == product.Id).FirstOrDefault();

                productToCart.CartId = orderModels.Id;
                productToCart.ProductId = product.Id;

                orderModels.TotalAmount = orderModels.TotalAmount - product.CurrentPrice;
                orderModels.NumberOfProducts = orderModels.NumberOfProducts - 1;

                db.ProductOrderModels.Remove(productToCart);
                db.SaveChanges();

                product.Stock = product.Stock + 1;
                db.SaveChanges();
            }

            string redirectUrl = "/Orders/Details/" + OredrId;
            return Redirect(redirectUrl);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
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
            return View(orderModels);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccountId,Email,TotalAmount,NumberOfProducts,Name,Phone,Address,City,State,Status")] OrderModels orderModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderModels);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
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
            return View(orderModels);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderModels orderModels = db.OrderModels.Find(id);
            db.OrderModels.Remove(orderModels);
            db.SaveChanges();
            return RedirectToAction("Index");
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
