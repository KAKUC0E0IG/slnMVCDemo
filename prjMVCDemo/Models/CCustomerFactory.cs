using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace prjMVCDemo.Models
{
    public class CCustomerFactory
    {
        public string math(int a, int b, int c)
        {
            double d = Math.Sqrt((b * b) - 4 * a * c);
            string ra = ((-b + d) / (2 * a)).ToString("0.00");
            string rb = ((-b - d) / (2 * a)).ToString("0.00");
            string s = ra + "或" + rb;
            return s;
        }
        public void delete(int fId)
        {
            string sql = "DELETE FROM tCustomer WHERE fId = @K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fId));
            executeSql(sql, paras);
        }
        public CCustomer queryById(int fId)
        {
            string sql = "SELECT * FROM tCustomer WHERE fId=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fId));
            List<CCustomer> list = queryBySql(sql, paras);
            if (list.Count == 0)
            {
                return null;
            }
            return list[0];
        }
        public List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = "SELECT * FROM tCustomer WHERE ";
            sql += "fName LIKE @K_KEYWORD ";
            sql += "OR fPhone LIKE @K_KEYWORD ";
            sql += "OR fEmail LIKE @K_KEYWORD ";
            sql += "OR fAddress LIKE @K_KEYWORD ";

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_KEYWORD","%"+(object)keyword+"%"));
            return queryBySql(sql, paras);
        }
        public List<CCustomer> queryAll()
        {
            string sql = "SELECT * FROM tCustomer";
            List<CCustomer> list = queryBySql(sql, null);
            if (list.Count == 0)
            {
                return null;
            }
            return list;
        }
        private List<CCustomer> queryBySql(string sql,List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["prjMVCDemo"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
            {
                cmd.Parameters.AddRange(paras.ToArray());
            }
            SqlDataReader reader = cmd.ExecuteReader();
            List<CCustomer> list = new List<CCustomer>();
            while (reader.Read())
            {
                CCustomer customer = new CCustomer()
                {
                    fId = (int)reader["fId"],
                    fName = reader["fName"].ToString(),
                    fPhone = reader["fPhone"].ToString(),
                    fEmail = reader["fEmail"].ToString(),
                    fAddress = reader["fAddress"].ToString(),
                    fPassword = reader["fPassword"].ToString()
                };
                list.Add(customer);
            }
            con.Close();
            return list;
        }
        public void create(CCustomer customer)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "INSERT INTO tCustomer (";
            if (!string.IsNullOrEmpty(customer.fName))
            { 
               sql += " fName,"; 
            }
            if (!string.IsNullOrEmpty(customer.fPhone))
            {
                sql += " fPhone,";
            }
            if (!string.IsNullOrEmpty(customer.fEmail))
            {
                sql += " fEmail,";
            }
            if (!string.IsNullOrEmpty(customer.fAddress))
            {
                sql += " fAddress,";
            }
            if (!string.IsNullOrEmpty(customer.fPassword))
            {
                sql += " fPassword,";
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
            { 
                sql=sql.Trim().Substring(0,sql.Trim().Length-1);
            }
            sql += " ) VALUES (";
            if (!string.IsNullOrEmpty(customer.fName))
            { 
                sql += "@K_FNAME,";
            paras.Add(new SqlParameter("K_FNAME", (object)customer.fName));
            }
            if (!string.IsNullOrEmpty(customer.fPhone))
            { 
                 sql += "@K_FPHONE,";
            paras.Add(new SqlParameter("K_FPHONE", (object)customer.fPhone));
            }
            if (!string.IsNullOrEmpty(customer.fEmail))
            { 
              sql += "@K_FEMAIL,";
            paras.Add(new SqlParameter("K_FEMAIL", (object)customer.fEmail));  
            }
            if (!string.IsNullOrEmpty(customer.fAddress))
            { 
               sql += "@K_FADDRESS,";
            paras.Add(new SqlParameter("K_FADDRESS", (object)customer.fAddress)); 
            }
            if (!string.IsNullOrEmpty(customer.fPassword))
            { 
                sql += "@K_FPASSWORD";
            paras.Add(new SqlParameter("K_FPASSWORD", (object)customer.fPassword));
            }
            sql += ")";

            executeSql(sql, paras);
        }
        public void update(CCustomer customer)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "UPDATE  tCustomer SET";
            if (!string.IsNullOrEmpty(customer.fName))
            {
                sql += " fName=@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)customer.fName));
            }
            if (!string.IsNullOrEmpty(customer.fPhone))
            {
                sql += " fPhone=@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (object)customer.fPhone));
            }
            if (!string.IsNullOrEmpty(customer.fEmail))
            {
                sql += "fEmail=@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (object)customer.fEmail));
            }
            if (!string.IsNullOrEmpty(customer.fAddress))
            {
                sql += "fAddress=@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (object)customer.fAddress));
            }
            if (!string.IsNullOrEmpty(customer.fPassword))
            {
                sql += "fPassword = @K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)customer.fPassword));
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
            {
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            }
            sql += " WHERE fId =@K_FID";
            paras.Add(new SqlParameter("K_FID", (object)customer.fId));

            executeSql(sql, paras);
        }
        private static void executeSql(string sql,List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["prjMVCDemo"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
            {
                cmd.Parameters.AddRange(paras.ToArray());
            }
            cmd.ExecuteNonQuery();
        }
    }
}