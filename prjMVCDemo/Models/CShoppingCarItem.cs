using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CShoppingCarItem
    {
        [DisplayName("商品編號")]
        public int productId { get; set; }
        [DisplayName("購買數量")]
        public int fCount { get; set; }
        [DisplayName("金額")]
        public decimal fPrice { get; set; }
        public decimal 小計 { get { return this.fCount * this.fPrice; } }
        public tProduct product { get; set; }
    }
}