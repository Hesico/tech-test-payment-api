using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.models
{
    public class Seller
    {

        public Seller(int id, string cpf, string name, string email, string phoneNumber){
            
            this.Id = id;
            this.Cpf = cpf;
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        [Required(ErrorMessage = "O Id do Vendedor é obrigatório e não pode ser igual à 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O CPF do Vendedor é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O Nome do Vendedor é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O E-mail Vendedor é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O número do telefone do Vendedor é obrigatório")]
        public string PhoneNumber { get; set; }
    }
}