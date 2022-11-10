using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.models
{
    public class OrderStorage
    {
        private List<Order> _Orders;
        private int _IdCount;

        public OrderStorage(){
            this._IdCount = 1;
            this._Orders = new List<Order>();
        }

        public Order GetOrderById(int id){
            var order  = this._Orders.Find(e => e.Id == id);

            if(order == null) throw new Exception("Venda não encontrada");

            return order;
        }

        public int AddOrder(Order newOrder){

            newOrder.SetId(this._IdCount++);
            var order  = this._Orders.Find(e => e.Id == newOrder.Id);

            if(order == null){
                this._Orders.Add(newOrder);
                return newOrder.Id;
            }else{
                throw new Exception("Ops. Não foi possível armazenar a venda. O id da venda é duplicado!");
            }
        }

        public void UpdateOrder(Order order){
            int orderIndex = this._Orders.FindIndex(e => e.Id == order.Id);
            
            this._Orders[orderIndex] = order;
            
        }
    }
}