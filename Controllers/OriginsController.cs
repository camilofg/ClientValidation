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
    public class OriginsController : Controller
    {
        private QValidationsRepository db = new QValidationsRepository();

        // GET: Origins
        public ActionResult Index()
        {
            var origins = db.Origins.Include(o => o.State1);
            return View(origins.ToList());
        }

        // GET: Origins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origin origin = db.Origins.Find(id);
            if (origin == null)
            {
                return HttpNotFound();
            }
            return View(origin);
        }

        // GET: Origins/Create
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States, "StateID", "StateName");
            return View();
        }

        // POST: Origins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrID,Origin1,State")] Origin origin)
        {
            if (ModelState.IsValid)
            {
                db.Origins.Add(origin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.State = new SelectList(db.States, "StateID", "StateName", origin.State);
            return View(origin);
        }

        // GET: Origins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origin origin = db.Origins.Find(id);
            if (origin == null)
            {
                return HttpNotFound();
            }
            ViewBag.State = new SelectList(db.States, "StateID", "StateName", origin.State);
            return View(origin);
        }

        // POST: Origins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrID,Origin1,State")] Origin origin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(origin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States, "StateID", "StateName", origin.State);
            return View(origin);
        }

        // GET: Origins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origin origin = db.Origins.Find(id);
            if (origin == null)
            {
                return HttpNotFound();
            }
            return View(origin);
        }

        // POST: Origins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Origin origin = db.Origins.Find(id);
            db.Origins.Remove(origin);
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
