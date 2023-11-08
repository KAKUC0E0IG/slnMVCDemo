using prjMauiDemo.Model;
using prjMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class AController : Controller
    {
        // GET: A
        static int count = 0;
        public ActionResult demoCountBySession()
        {
            int count = 0;
            if (Session["count"] != null)
            {
                count= (int)Session["count"];
            }
            count++;
            Session["count"] = count;
            ViewBag.Count = count;
            return View();
        }
        public ActionResult demoCount() 
        {
            count++;
            ViewBag.Count = count;
            return View();
        }

        public ActionResult demoForm()
        {
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                int a = Convert.ToInt32(Request.Form["txtA"]);
                int b = Convert.ToInt32(Request.Form["txtB"]);
                int c = Convert.ToInt32(Request.Form["txtC"]);
                ViewBag.ANS =  (new CCustomerFactory()).math(a, b, c);
            }
            return View();
        }

        public string testingQuery()
        { 
            return "目前總共有: "+(new CCustomerFactory()).queryAll().Count().ToString()+"名客戶";
        }
        public string testingDelete(int? id)
        {
            if (id == null)
            {
                return "沒有指定Id";
            }
            (new CCustomerFactory()).delete((int)id);
            return "刪除資料成功";
        }
        public string testingInsert()
        { 
            CCustomer customer = new CCustomer();
            customer.fName = "test";
            customer.fPhone = "4564567890";
            customer.fEmail = "001@gmail.com";
            customer.fAddress = "Taipei";
            customer.fPassword = "123";
            (new CCustomerFactory()).create(customer);
            return "新增資料成功";
        }
        public string testingUpdate()
        {
            CCustomer customer = new CCustomer();
            customer.fId = 6;
            customer.fName = "test222";
            customer.fPhone = "4564567890";
            customer.fEmail = "001@gmail.com";
            customer.fAddress = "Kaoshung";
            customer.fPassword = "123";
            (new CCustomerFactory()).update(customer);
            return "修改資料成功";
        }
        public string sayHello()
        {
            return "Hello World!";
        }

        public string lotto()
        {
            return (new CLotto()).lotto();
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\Image\001.jpg");
            Response.End();
            return "";
        }

        public string demoRequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "1")
            {
                return "XBOX已加入購物車";
            }
            else if (id == "2")
            {
                return "PS5已加入購物車";
            }
            else if (id == "3")
            {
                return "SWITCH已加入購物車";
            }
            else
            {
                return "找不到該商品";
            }
        }

        public string demoParameter(int? id)
        {
            if (id == 1)
            {
                return "XBOX已加入購物車";
            }
            else if (id == 2)
            {
                return "PS5已加入購物車";
            }
            else if (id == 3)
            {
                return "SWITCH已加入購物車";
            }
            else
            {
                return "找不到該商品";
            }
        }

        public ActionResult bindingCustomer(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["prjMVCDemo"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    x = new CCustomer()
                    {
                        fId = (int)r["fId"],
                        fName = r["fName"].ToString(),
                        fPhone = r["fPhone"].ToString()
                    };
                }

                con.Close();
            }
            return View(x);
        }
        public ActionResult displayqueryCustomer(int? id)
        {
            if (id != null)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["prjMVCDemo"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    CCustomer x = new CCustomer()
                    {
                        fId = (int)r["fId"],
                        fName = r["fName"].ToString(),
                        fPhone = r["fPhone"].ToString()
                    };
                ViewBag.KK = x;
                }

                con.Close();
            }
            return View();
        }
        public string queryCustomer(int? id)
        {
            if (id != null)
            {
                ViewBag.KK = "沒有符合查詢的資料";

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["prjMVCDemo"].ToString());
                //con.ConnectionString = @"Data Source=.;Initial Catalog=ShopTest;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    ViewBag.KK = r["fName"].ToString() + "\b" + r["fPhone"].ToString();
                }

                con.Close();
            }
            else
            { 
                ViewBag.KK = "沒有指定id";
            }
            return ViewBag.KK;
        }
    }
}