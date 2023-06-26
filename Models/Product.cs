﻿using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string Image { get; set; }
    }
}
