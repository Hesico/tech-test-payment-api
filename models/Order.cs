using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.models
{
    public class Order
    {
        private int _Id;
        public int Id
        {
            get => _Id;
        }

        public Seller Seller { get; set; }
        public List<Product> Products { get; set; }
        public DateTime Date { get; private set; }

        private EnumStatusOrder _Status;
        public string Status
        {
            get => _Status.ToString();
        }

        public Order(int id, Seller seller,List<Product> products){
            this._Id = id;
            this.Seller = seller;
            this.Products = products;
            this.Date = DateTime.Now;
            this._Status = EnumStatusOrder.Aguardando_pagamento;
        }

        public void SetId(int id){
            this._Id = id;
        }

        public void UpdateOrderStatus(EnumStatusOrder newStatus){
            IDictionary<EnumStatusOrder, List<EnumStatusOrder>> allowedTransitions = new Dictionary<EnumStatusOrder, List<EnumStatusOrder>> (){
                {EnumStatusOrder.Aguardando_pagamento,new List<EnumStatusOrder>{EnumStatusOrder.Pagamento_aprovado,EnumStatusOrder.Cancelada}},
                {EnumStatusOrder.Pagamento_aprovado,new List<EnumStatusOrder>{EnumStatusOrder.Enviado_para_transportadora,EnumStatusOrder.Cancelada}},
                {EnumStatusOrder.Enviado_para_transportadora,new List<EnumStatusOrder>{EnumStatusOrder.Entregue}},
            };
            
            var isAbleToChange = allowedTransitions.TryGetValue(this._Status, out var ableStatus);

            if(isAbleToChange == false) throw new ArgumentException("O Status Atual não pode ser alterado!");
            
            var isNewStatusAble = ableStatus.Find(e => e == newStatus);

            if(isNewStatusAble == EnumStatusOrder.none) throw new ArgumentException("O Status atual não pode ser mudado para o novo Status!");

            this._Status = newStatus;
        }

        public void UpdateOrderStatusTest(EnumStatusOrder newStatus){
            this._Status = newStatus;
        }
    }
}