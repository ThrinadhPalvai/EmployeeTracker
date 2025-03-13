using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LoginPagePeoject;

namespace LoginPagePeoject.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        private proteckhydbEntities1 db = new proteckhydbEntities1();

        // GET: EmployeeDetails
        public ActionResult Index()
        {
            var employeeDetails = db.EmployeeDetails.Include(e => e.DesignTb);
            return View(employeeDetails.ToList());
        }

        // GET: EmployeeDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetail employeeDetail = db.EmployeeDetails.Find(id);
            if (employeeDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeDetail);
        }

        // GET: EmployeeDetails/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeType = new SelectList(GetEmployeeTypes(), "Value", "Text");
            ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation");
            ViewBag.Department = new SelectList(db.Department1, "DeptId", "Department");
            return View();
        }

        private IEnumerable<SelectListItem> GetEmployeeTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Full-Time" },
                new SelectListItem { Value = "2", Text = "Part-Time" },
                new SelectListItem { Value = "3", Text = "Freelance" },
                new SelectListItem { Value = "4", Text = "Intern" },
                new SelectListItem { Value = "5", Text = "ContractEmployee" },
                // Add more types as needed
            };
        }

        // POST: EmployeeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDetail employeeDetail, registration reg, loginpage logPage)
        {
            if (ModelState.IsValid)
            {
                db.loginpages.Add(logPage);
                db.registrations.Add(reg);
                db.EmployeeDetails.Add(employeeDetail);

                reg.Id = employeeDetail.ID;
                reg.Password = "Pro00" + employeeDetail.ID;
                reg.Username = employeeDetail.Email;
                reg.DateofBirth = employeeDetail.DateOfBirth;
                reg.Email = employeeDetail.Email;
                reg.PhoneNumber = employeeDetail.PhoneNumber;
                reg.Address = employeeDetail.City;
                reg.Designation = employeeDetail.Designation;

                logPage.Designation = employeeDetail.Designation.ToString();
                logPage.Id = employeeDetail.ID;
                logPage.Username = employeeDetail.Email;
                logPage.Password = "Pro00" + employeeDetail.ID;
                logPage.ActiveStatus = employeeDetail.ActiveStatus == true;



                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeType = new SelectList(GetEmployeeTypes(), "Value", "Text", employeeDetail.EmployeeType);
            ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation", employeeDetail.Designation);
            return View(employeeDetail);
        }

        // GET: EmployeeDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetail employeeDetail = db.EmployeeDetails.Find(id);
            if (employeeDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeType = new SelectList(GetEmployeeTypes(), "Value", "Text", employeeDetail.EmployeeType);
            ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation", employeeDetail.Designation);
            return View(employeeDetail);
        }

        // POST: EmployeeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeType = new SelectList(GetEmployeeTypes(), "Value", "Text", employeeDetail.EmployeeType);
            ViewBag.Designation = new SelectList(db.DesignTbs, "Id", "Designation", employeeDetail.Designation);
            return View(employeeDetail);
        }

        // GET: EmployeeDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetail employeeDetail = db.EmployeeDetails.Find(id);
            if (employeeDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeDetail);
        }

        // POST: EmployeeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeDetail employeeDetail = db.EmployeeDetails.Find(id);
            db.EmployeeDetails.Remove(employeeDetail);
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
