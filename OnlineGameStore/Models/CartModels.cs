using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineGameStore.Models
{
    public class CartModels
    {
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
        public float TotalAmount { get; set; }
        public int NumberOfProducts { get; set; }
    }
}