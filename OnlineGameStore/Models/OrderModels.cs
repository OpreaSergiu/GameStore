﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineGameStore.Models
{
    public class OrderModels
    {
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
        public float TotalAmount { get; set; }
        public int NumberOfProducts { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
    }
}