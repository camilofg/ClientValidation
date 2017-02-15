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
    public class ArtisansController : Controller
    {
        private QValidationsRepository db = new QValidationsRepository();

        // GET: Artisans
        public ActionResult Index()
        {
            return View(db.Artisans.ToList());
        }

        // GET: Artisans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artisan artisan = db.Artisans.Find(id);
            if (artisan == null)
            {
                return HttpNotFound();
            }
            return View(artisan);
        }

        // GET: Artisans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artisans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtisanID,ArtisanName,ArtisanDocNumber,ArtisanDocType,ArtisanAddress,ArtisanTel")] Artisan artisan)
        {
            if (ModelState.IsValid)
            {
                db.Artisans.Add(artisan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artisan);
        }

        // GET: Artisans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artisan artisan = db.Artisans.Find(id);
            if (artisan == null)
            {
                return HttpNotFound();
            }
            return View(artisan);
        }

        // POST: Artisans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtisanID,ArtisanName,ArtisanDocNumber,ArtisanDocType,ArtisanAddress,ArtisanTel")] Artisan artisan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artisan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artisan);
        }

        // GET: Artisans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artisan artisan = db.Artisans.Find(id);
            if (artisan == null)
            {
                return HttpNotFound();
            }
            return View(artisan);
        }

        // POST: Artisans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artisan artisan = db.Artisans.Find(id);
            db.Artisans.Remove(artisan);
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
