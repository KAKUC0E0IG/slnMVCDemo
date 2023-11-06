using prjMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult List()
        {
            List<CCustomer> datas = (new CCustomerFactory()).queryAll();
            return View(datas);
        }
    }
}