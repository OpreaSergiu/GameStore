using Microsoft.AspNet.Identity;
using OnlineGameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineGameStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            string user_name = User.Identity.GetUserName();

            var resultCart = db.CartModels.SingleOrDefault(b => b.Email == user_name);
            var resultProductsJoin = db.ProductsChartModels.Where(s => s.CartId == resultCart.Id).ToList();

            var resultProducts = db.ProductsModels.Join(db.ProductsChartModels, s => s.Id, sa => sa.ProductId, (s, sa) => new { products = s, productsJoin = sa }).Select(ssa => ssa.products);

            var model = new CartViewModels()
            {
                Cart = resultCart,

                ProductsJoin = resultProductsJoin,

                Products = resultProducts
            };

            return View(model);
        }

        public ActionResult DeleteFromCart(int id)
        {
            string user_name = User.Identity.GetUserName();

            var result = db.CartModels.SingleOrDefault(b => b.Email == user_name);


            if (result != null)
            {
                var product = db.ProductsModels.Find(id);
                var productToCart = db.ProductsChartModels.Where(s => s.CartId == result.Id).Where(b => b.ProductId == product.Id).FirstOrDefault();

                productToCart.CartId = result.Id;
                productToCart.ProductId = product.Id;

                result.TotalAmount = result.TotalAmount - product.CurrentPrice;
                result.NumberOfProducts = result.NumberOfProducts - 1;

                db.ProductsChartModels.Remove(productToCart);
                db.SaveChanges();
            }

            string redirectUrl = "/Cart/Index";
            return Redirect(redirectUrl);
        }

        public ActionResult PlaceOrder(int id)
        {
            string Name = User.Identity.GetUserName();

            var result = db.CartModels.Find(id);

            AccountInfoModels accountInfoModels = db.AccountInfoModels.Where(s => s.Email == Name).FirstOrDefault();

            if (accountInfoModels.Phone == null)
            {
                string redirectUrl = "/Manage/Index";
                return Redirect(redirectUrl);
            }
            else
            {
                if (result != null)
                {
                    var productToCart = db.ProductsChartModels.Where(b => b.CartId == id).ToList();

                    var userOrder = new OrderModels();

                    userOrder.AccountId = " ";
                    userOrder.Email = Name;
                    userOrder.TotalAmount = result.TotalAmount;
                    userOrder.NumberOfProducts = result.NumberOfProducts;
                    userOrder.Name = accountInfoModels.Name;
                    userOrder.Address = accountInfoModels.Address;
                    userOrder.City = accountInfoModels.City;
                    userOrder.State = accountInfoModels.State;
                    userOrder.Phone = accountInfoModels.Phone;

                    db.OrderModels.Add(userOrder);
                    db.SaveChanges();

                    var userOrderAftePlacement = db.OrderModels.Where(b => b.Email == Name).Where(b => b.Name == accountInfoModels.Name).OrderByDescending(s => s.Id).Take(1).FirstOrDefault();

                    foreach (var item in productToCart)
                    {
                        var product = db.ProductsModels.Find(item.ProductId);

                        var productToOrder = new ProductOrderModels();

                        productToOrder.CartId = userOrderAftePlacement.Id;
                        productToOrder.ProductId = product.Id;

                        db.ProductOrderModels.Add(productToOrder);
                        db.SaveChanges();

                        product.Stock = product.Stock - 1;

                        db.ProductsChartModels.Remove(item);
                        db.SaveChanges();
                    }

                    result.TotalAmount = 0;
                    result.NumberOfProducts = 0;

                    db.SaveChanges();
                }
            } 

            string redirectUrl1 = "/Cart/Index";
            return Redirect(redirectUrl1);
        }
    }
}