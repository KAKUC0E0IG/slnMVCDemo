using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyWord"];
            ShopTestEntities db = new ShopTestEntities();
            IEnumerable<tProduct> datas = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                datas = db.tProducts.Where(p => p.fName.Contains(keyword));
            }
            else
            { 
            datas = from x in db.tProducts
                        select x;
            }
            return View(datas);
        }

        public ActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Create(tProduct p)
        { 
            ShopTestEntities db = new ShopTestEntities();
            db.tProducts.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            ShopTestEntities db = new ShopTestEntities();
            tProduct product = db.tProducts.FirstOrDefault(p => p.fId == id);
            if (product != null)
            { 
                db.tProducts.Remove(product);
                db.SaveChanges();
            }
           return RedirectToAction("List");
        }

        public ActionResult Edit(int? id) 
        {
            ShopTestEntities db = new ShopTestEntities();
            tProduct p = db.tProducts.FirstOrDefault(x => x.fId == id);
            if (p == null) 
            {
                return RedirectToAction("List");
            }
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(tProduct p) 
        {
            ShopTestEntities db = new ShopTestEntities();
            tProduct product = db.tProducts.FirstOrDefault(x => x.fId == p.fId);
            if (p != null)
            {
                p.fName = product.fName;
                p.fQty = product.fQty;
                p.fPrice = product.fPrice;
                p.fCost = product.fCost;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}