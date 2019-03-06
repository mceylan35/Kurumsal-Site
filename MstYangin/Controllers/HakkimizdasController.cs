using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MstYangin.Models;

namespace MstYangin.Controllers
{
    public class HakkimizdasController : Controller
    {
        private MstYanginContext db = new MstYanginContext();

        // GET: Hakkimizdas
        public ActionResult Index()
        {
            return View(db.Hakkimizda.ToList());
        }

        // GET: Hakkimizdas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hakkimizda hakkimizda = db.Hakkimizda.Find(id);
            if (hakkimizda == null)
            {
                return HttpNotFound();
            }
            return View(hakkimizda);
        }

        // GET: Hakkimizdas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hakkimizdas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Icerik,Baslik,Aciklama")] Hakkimizda hakkimizda)
        {
            if (ModelState.IsValid)
            {
                db.Hakkimizda.Add(hakkimizda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hakkimizda);
        }

        // GET: Hakkimizdas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hakkimizda hakkimizda = db.Hakkimizda.Find(id);
            if (hakkimizda == null)
            {
                return HttpNotFound();
            }
            return View(hakkimizda);
        }

        // POST: Hakkimizdas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Icerik,Baslik,Aciklama")] Hakkimizda hakkimizda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hakkimizda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hakkimizda);
        }

        // GET: Hakkimizdas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hakkimizda hakkimizda = db.Hakkimizda.Find(id);
            if (hakkimizda == null)
            {
                return HttpNotFound();
            }
            return View(hakkimizda);
        }

        // POST: Hakkimizdas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hakkimizda hakkimizda = db.Hakkimizda.Find(id);
            db.Hakkimizda.Remove(hakkimizda);
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
