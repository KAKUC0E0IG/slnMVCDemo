using prjMauiDemo.Model;
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
        public ActionResult Index()
        {
            return View();
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

        public ActionResult displayqueryCustomer(int? id)
        {
            return View();
        }
        public string queryCustomer(int? id)
        {
            if (id == null)
            {
                return "沒有指定id";
            }
            string s = "沒有符合查詢的資料";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["prjMVCDemo"].ToString());
            //con.ConnectionString = @"Data Source=.;Initial Catalog=ShopTest;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId="+id.ToString(),con);
            SqlDataReader r = cmd.ExecuteReader();

            if (r.Read())
            {
                s = r["fName"].ToString() + "/" + r["fPhone"].ToString();
            }

            con.Close();
            return s;
        }
    }
}