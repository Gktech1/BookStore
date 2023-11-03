using Cart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.DTOs
{
    public class ShoppingCartRespone
    {
        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        public List<ShoppingCartItem> Items { get; set; } 
    }
}
