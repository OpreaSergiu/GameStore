using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineGameStore.Models
{
    public class CartViewModels
    {
        public CartModels Cart { get; set; }
        public IEnumerable<ProductsChartModels> ProductsJoin { get; set; }
        public IEnumerable<ProductsModels> Products { get; set; }
    }
}