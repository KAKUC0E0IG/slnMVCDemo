using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CCustomer
    {
        [DisplayName("客戶編號")]
        public int fId { get; set; }
        [DisplayName("姓名")]
        public string fName { get; set; }
        [DisplayName("手機號碼")]
        public string fPhone { get; set; }
        [DisplayName("電子郵件")]
        public string fEmail { get; set; }
        [DisplayName("住址")]
        public string fAddress { get; set; }
        [DisplayName("密碼")]
        public string fPassword { get; set; }
    }
}