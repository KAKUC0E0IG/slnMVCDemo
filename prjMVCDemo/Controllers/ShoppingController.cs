using prjMVCDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            ShopTestEntities db = new ShopTestEntities();
            var datas = from x in db.tProducts
                    select x;
            return View(datas);
        }

        public ActionResult AddToCar(int? id) 
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            ViewBag.FID=id;
            return View();
        }

        [HttpPost]
        public ActionResult AddToCar(CAddToCarViewModel vm)
        {
            ShopTestEntities db = new ShopTestEntities();
            tProduct product = db.tProducts.FirstOrDefault(p => p.fId == vm.txtFID);
            if (product == null)
            {
                return RedirectToAction("List");
            }
            tShoppingCar car = new tShoppingCar()
            { 
                fProductId = vm.txtFID,
                fPrice = product.fPrice,
                fCount = vm.txtCount,
                fCustomerId = 1,
                fDate = DateTime.Now.ToString("yyyyMMddHHmmss")
            };
            db.tShoppingCars.Add(car);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult demoUpload()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult demoUpload(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\msit\ASPNET\slnMVCDemo\prjMVCDemo\Image\hello.png");
            return View();
        }
    }
}