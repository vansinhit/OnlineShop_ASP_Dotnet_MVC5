using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [Serializable] // để tuần tự hóa nó
    public class CartItem
    {
        public Product Product { set; get; } // ta gọi trực tiếp đến thèn Product
        public int Quantity { set; get; }
      
    }
}