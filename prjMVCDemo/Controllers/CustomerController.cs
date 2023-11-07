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
            string keyword = Request.Form["txtKeyword"];
            List<CCustomer> datas = null;
            if (string.IsNullOrEmpty(keyword))
            {
                datas = (new CCustomerFactory()).queryAll();
            }
            else 
            {
                datas = (new CCustomerFactory()).queryByKeyword(keyword);
            }   
            return View(datas);
        }

        public ActionResult create()
        {
            return View();
        }
        public ActionResult Save()
        {
            CCustomer customer = new CCustomer();
            customer.fName = Request.Form["txtName"];
            customer.fPhone = Request.Form["txtPhone"];
            customer.fEmail = Request.Form["txtEmail"];
            customer.fAddress = Request.Form["txtAddress"];
            customer.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).create(customer);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                (new CCustomerFactory()).delete((int)id);
            }
            return RedirectToAction("List");
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            CCustomer customer = (new CCustomerFactory()).queryById((int)id);
            if (customer == null)
            {
                return RedirectToAction("List");
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult Update(CCustomer customer)
        {
            (new CCustomerFactory()).update(customer);
            return RedirectToAction("List");
        }

    }
}