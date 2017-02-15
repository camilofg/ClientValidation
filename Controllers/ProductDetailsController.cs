using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository;

namespace ClientValidation.Controllers
{
    [Authorize]
    public class ProductDetailsController : Controller
    {
        private QValidationsRepository db = new QValidationsRepository();

        // GET: ProductDetails1
        public ActionResult Index()
        {
            var productDetails = db.ProductDetails.Include(p => p.Artisan).Include(p => p.Material).Include(p => p.Origin).Include(p => p.InInventory);
            return View(productDetails.ToList());
        }

        [Authorize]
        // GET: ProductDetails1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productDetail = db.GUID_Encrypt_Base64(id).FirstOrDefault();//.ProductDetails.Find(id);
           
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // GET: ProductDetails1/Create
        public ActionResult Create()
        {
            ViewBag.ProdArtisan = new SelectList(db.Artisans, "ArtisanID", "ArtisanName");
            ViewBag.ProdMaterial = new SelectList(db.Materials, "MatID", "Material1");
            ViewBag.ProdOrigin = new SelectList(db.Origins, "OrID", "Origin1");
            ViewBag.ProdID = new SelectList(db.InInventories, "InInvID", "InInvGUID");
            return View();
        }

        // POST: ProductDetails1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdID,ProdSerial,ProdName,ProdReview,ProdOrigin,ProdMaterial,ProdArtisan")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                db.ProductDetails.Add(productDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProdArtisan = new SelectList(db.Artisans, "ArtisanID", "ArtisanName", productDetail.ProdArtisan);
            ViewBag.ProdMaterial = new SelectList(db.Materials, "MatID", "Material1", productDetail.ProdMaterial);
            ViewBag.ProdOrigin = new SelectList(db.Origins, "OrID", "Origin1", productDetail.ProdOrigin);
            ViewBag.ProdID = new SelectList(db.InInventories, "InInvID", "InInvGUID", productDetail.ProdID);
            return View(productDetail);
        }

        // GET: ProductDetails1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = db.ProductDetails.Find(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdArtisan = new SelectList(db.Artisans, "ArtisanID", "ArtisanName", productDetail.ProdArtisan);
            ViewBag.ProdMaterial = new SelectList(db.Materials, "MatID", "Material1", productDetail.ProdMaterial);
            ViewBag.ProdOrigin = new SelectList(db.Origins, "OrID", "Origin1", productDetail.ProdOrigin);
            ViewBag.ProdID = new SelectList(db.InInventories, "InInvID", "InInvGUID", productDetail.ProdID);
            return View(productDetail);
        }

        // POST: ProductDetails1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdID,ProdSerial,ProdName,ProdReview,ProdOrigin,ProdMaterial,ProdArtisan")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProdArtisan = new SelectList(db.Artisans, "ArtisanID", "ArtisanName", productDetail.ProdArtisan);
            ViewBag.ProdMaterial = new SelectList(db.Materials, "MatID", "Material1", productDetail.ProdMaterial);
            ViewBag.ProdOrigin = new SelectList(db.Origins, "OrID", "Origin1", productDetail.ProdOrigin);
            ViewBag.ProdID = new SelectList(db.InInventories, "InInvID", "InInvGUID", productDetail.ProdID);
            return View(productDetail);
        }

        // GET: ProductDetails1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = db.ProductDetails.Find(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // POST: ProductDetails1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductDetail productDetail = db.ProductDetails.Find(id);
            db.ProductDetails.Remove(productDetail);
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
