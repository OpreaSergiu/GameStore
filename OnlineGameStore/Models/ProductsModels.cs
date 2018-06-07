using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineGameStore.Models
{
    public class ProductsModels
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float NormalPriece { get; set; }
        public float CurrentPrice { get; set; }
        public int Stock { get; set; }
        public string Details { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AddDate { get; set; }
        public string Pruducer { get; set; }
        public string Type { get; set; }
        public string ImagePath { get; set; }
    }
}