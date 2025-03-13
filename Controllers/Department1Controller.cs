using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoginPagePeoject;

namespace LoginPagePeoject.Controllers
{
    public class Department1Controller : Controller
    {
        private proteckhydbEntities1 db = new proteckhydbEntities1();

        // GET: Department1
        public ActionResult Index()
        {
            return View(db.Department1.ToList());
        }

        // GET: Department1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department1 department1 = db.Department1.Find(id);
            if (department1 == null)
            {
                return HttpNotFound();
            }
            return View(department1);
        }

        // GET: Department1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department1 department1)
        {
            if (ModelState.IsValid)
            {
                db.Department1.Add(department1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department1);
        }

        // GET: Department1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department1 department1 = db.Department1.Find(id);
            if (department1 == null)
            {
                return HttpNotFound();
            }
            return View(department1);
        }

        // POST: Department1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department1 department1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department1);
        }

        // GET: Department1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department1 department1 = db.Department1.Find(id);
            if (department1 == null)
            {
                return HttpNotFound();
            }
            return View(department1);
        }

        // POST: Department1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department1 department1 = db.Department1.Find(id);
            db.Department1.Remove(department1);
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
