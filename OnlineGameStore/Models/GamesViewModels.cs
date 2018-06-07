using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineGameStore.Models
{
    public class GamesViewModels
    {
        public IEnumerable<ProductsModels> Products { get; set; }
    }
}