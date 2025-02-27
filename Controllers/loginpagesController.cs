using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LoginPagePeoject;

namespace LoginPagePeoject.Controllers
{
    public class loginpagesController : Controller
    {
        private proteckhydbEntities db = new proteckhydbEntities();

        // GET: loginpages
        public ActionResult Index()
        {
            return View(db.loginpages.ToList());
        }

        // GET: loginpages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loginpage loginpage = db.loginpages.Find(id);
            if (loginpage == null)
            {
                return HttpNotFound();
            }
            return View(loginpage);
        }

        // GET: loginpages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: loginpages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password")] loginpage loginpage)
        {
            //if (ModelState.IsValid)
            //{
            //    db.loginpages.Add(loginpage);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            var user=db.loginpages.FirstOrDefault(x=>x.Username==loginpage.Username&& x.Password==loginpage.Password);
            if (user != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //ModelState.AddModelError("", "Invalid Credentials");
                ViewBag.validation = "Invalid Credentials";
            }
            return View(loginpage);
        }

        // GET: loginpages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loginpage loginpage = db.loginpages.Find(id);
            if (loginpage == null)
            {
                return HttpNotFound();
            }
            return View(loginpage);
        }

        // POST: loginpages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password")] loginpage loginpage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginpage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loginpage);
        }

        // GET: loginpages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loginpage loginpage = db.loginpages.Find(id);
            if (loginpage == null)
            {
                return HttpNotFound();
            }
            return View(loginpage);
        }

        // POST: loginpages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            loginpage loginpage = db.loginpages.Find(id);
            db.loginpages.Remove(loginpage);
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();
        }
    }
}
