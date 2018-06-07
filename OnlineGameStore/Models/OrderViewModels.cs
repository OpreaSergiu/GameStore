using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineGameStore.Models
{
    public class OrderViewModels
    {
        public OrderModels Order { get; set; }
        public IEnumerable<ProductOrderModels> ProductsJoin { get; set; }
        public IEnumerable<ProductsModels> Products { get; set; }
    }
}