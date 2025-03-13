using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginPagePeoject.Controllers
{
    public class HRController : Controller
    {
        private proteckhydbEntities1 db = new proteckhydbEntities1();
        // GET: HR
        public ActionResult Index()
        {
            var employeeDetails = db.EmployeeDetails.Include("DesignTb");
            return View(employeeDetails.ToList());
        }
    }
}