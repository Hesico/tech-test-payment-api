using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tech_test_payment_api.models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        
        private OrderStorage OrderStorage;

        public OrderController(){

            // A Classe OrderStorage é utilizada para simular a persistência dos dados
            // Nela se encontra métodos para Create, Read e Update. 
            this.OrderStorage = new OrderStorage();

            // Dados iniciais de Vendas para testes unitários
            Product produto1 = new Product(1,"produto 1", 10M);
            Product produto2 = new Product(2,"produto 2", 20M);
            Product produto3 = new Product(3,"produto 3", 30M);
            Product produto4 = new Product(4,"produto 4", 40M);

            Seller seller1 = new Seller(1,"cpf 1", "nome 1","email 1", "phone 1");
            Seller seller2 = new Seller(2,"cpf 2", "nome 2","email 2", "phone 2");
            Seller seller3 = new Seller(3,"cpf 3", "nome 3","email 3", "phone 3");

            List<Product> produtos1 = new List<Product>();
            produtos1.Add(produto1);
            produtos1.Add(produto3);
            produtos1.Add(produto4);

            List<Product> produtos2 = new List<Product>();
            produtos2.Add(produto2);
            produtos2.Add(produto4);
            produtos2.Add(produto1);

            List<Product> produtos3 = new List<Product>();
            produtos3.Add(produto3);
            produtos3.Add(produto4);
            produtos3.Add(produto1);

            Order order1 = new Order(1,seller2, produtos3);
            order1.UpdateOrderStatusTest(EnumStatusOrder.Pagamento_aprovado);

            Order order2 = new Order(2,seller3, produtos2);
            order2.UpdateOrderStatusTest(EnumStatusOrder.Enviado_para_transportadora);

            Order order3 = new Order(3,seller1, produtos1);
            order3.UpdateOrderStatusTest(EnumStatusOrder.Cancelada);

            Order order4 = new Order(4,seller1, produtos2);
            
            Order order5 = new Order(5,seller1, produtos1);
            order5.UpdateOrderStatusTest(EnumStatusOrder.Entregue);

            this.OrderStorage.AddOrder(order1);
            this.OrderStorage.AddOrder(order2);
            this.OrderStorage.AddOrder(order3);
            this.OrderStorage.AddOrder(order4);
            this.OrderStorage.AddOrder(order5);

        }

        [HttpPost("RegistrarVenda")]
        public IActionResult RegisterOrder(Order newOrder){
            
            if(newOrder.Products.Count == 0) return UnprocessableEntity("Não é possível cadastrar uma venda sem produtos!");
            if(newOrder.Products.Any(e => e.Id == 0)) return UnprocessableEntity("O Id do produto é obrigatório e não pode ser igual a 0!");
            if(newOrder.Seller.Id == 0) return UnprocessableEntity("O Id do Vendedor é obrigatório e não pode ser igual a 0!");

            int newOrderId = this.OrderStorage.AddOrder(newOrder);
            return Ok(this.OrderStorage.GetOrderById(newOrderId));
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id){
            try{
                Order order = this.OrderStorage.GetOrderById(id);
                return Ok(order);
            }catch(Exception e){
                return NotFound(e.Message);
            }
        }

        [HttpPut("AtualizarVenda/{id}")]
        public IActionResult UpdateOrder(int id, EnumStatusOrder newStatus){
            
            try{
                Order order = this.OrderStorage.GetOrderById(id);
                order.UpdateOrderStatus(newStatus);

                this.OrderStorage.UpdateOrder(order);
                return Ok(this.OrderStorage.GetOrderById(id));

            }catch(ArgumentException e){
                return UnprocessableEntity(e.Message);
            }catch(Exception e){
                return NotFound(e.Message);
            }
        }
    }
}