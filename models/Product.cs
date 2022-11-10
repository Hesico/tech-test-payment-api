using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.models
{
    public class Product
    {
        public Product (int id, string name, decimal price){
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        [Required(ErrorMessage = "O Id do produto é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome do produto é obrigatório")]
        public string Name { get; set; }
        
        public decimal Price { get; set; }
    }
}