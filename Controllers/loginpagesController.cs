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
        private proteckhydbEntities1 db = new proteckhydbEntities1();

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
       // [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(loginpage loginpage)
        {
            proteckhydbEntities1 db = new proteckhydbEntities1();

            var emplist = db.EmployeeDetails.ToList();


            var user = db.loginpages.FirstOrDefault(x => x.Username == loginpage.Username && x.Password == loginpage.Password && x.ActiveStatus ==true);

            if (user != null)
            {
                if (user.ActiveStatus == true)
                {
                    if (user.Designation == "1004")
                    {
                        return RedirectToAction("Index", "Lead");
                    }
                    else if (user.Designation == "1008")
                    {
                        return RedirectToAction("Index", "HR");
                    }
                    else if (user.Designation == "1005")
                    {
                        return RedirectToAction("Index", "Manager");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Handle inactive user
                    ViewBag.validation = "The User Credentials are in Inactive Status. Please contact Admin.";
                    return View(loginpage);
                }
            }
            else
            {
                // Handle invalid credentials
                ViewBag.validation = "Invalid Credentials";
                return View(loginpage);
            }
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
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Designation")] loginpage loginpage)
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
            return RedirectToAction("Logout");
        }
    }
}
