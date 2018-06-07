using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineGameStore.Models;

namespace OnlineGameStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.ProductsModels.ToList());
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,NormalPriece,CurrentPrice,Stock,Details,Pruducer,Type")] ProductsModels productsModels)
        {
            if (ModelState.IsValid)
            {
                productsModels.AddDate = DateTime.Now;
                var name = productsModels.ProductName;
                var price = productsModels.NormalPriece;
                db.ProductsModels.Add(productsModels);
                db.SaveChanges();

                var product = db.ProductsModels.Where(s => s.ProductName == name).Where(d => d.NormalPriece == price).SingleOrDefault();

                string path1 = Server.MapPath("~/ProductsImages/") + product.Id + "/";

                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(path1);
                }


                product.ImagePath = path1;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(productsModels);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,NormalPriece,CurrentPrice,Stock,Details,AddDate,Pruducer,Type,ImagePath")] ProductsModels productsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productsModels);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductsModels productsModels = db.ProductsModels.Find(id);
            db.ProductsModels.Remove(productsModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UploadFiles(int Id)
        {
            ViewBag.GameName = db.ProductsModels.Find(Id).ProductName;

            return View();
        }


        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase postedFile, int Id)
        {
            if (postedFile != null)
            {
                string folder_path = db.ProductsModels.Find(Id).ImagePath;

                if (!Directory.Exists(folder_path))
                {
                    Directory.CreateDirectory(folder_path);
                }

                postedFile.SaveAs(folder_path + "1.jpg");
                ViewBag.Message = "File uploaded successfully.";
            }

            return View();
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
