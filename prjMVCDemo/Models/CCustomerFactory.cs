using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CCustomerFactory
    {
        public void create(CCustomer customer)
        {
            string sql = "INSERT INTO tCustomer (";
            sql += " fName,";
            sql += " fPhone,";
            sql += " fEmail,";
            sql += " fAddress,";
            sql += " fPassword";
            sql += " ) VALUES (";
            sql += "'"+customer.fName+"',";
            sql += "'" + customer.fPhone + "',";
            sql += "'" + customer.fEmail + "',";
            sql += "'" + customer.fAddress + "',";
            sql += "'" + customer.fPassword + "')";

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["prjMVCDemo"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
    }
}