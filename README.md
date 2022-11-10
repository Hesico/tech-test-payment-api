## Evidências do Funcionamento

**Para realizar os testes dos métodos disponibilidos pela API, foram inicializadas instâncias de Vendas no construtor do OrderController:**

```    
this.OrderStorage = new OrderStorage();

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

```


Como pode-se notar, há 5 vendas registradas em memória, todas com Id, Vendedores e Produtos. Além disso, foi disponibilido um método não acessivel externamente, **UpdateOrderStatusTest**, que altera diretamente o Status do pedido. Desta forma, há uma venda cadastrada para cada Status de venda disponível.


---

:mag: **Teste de Leitura de Vendas**


Para esse teste, foram feitas requisições via Swagger pelos Id 1,2,3,4,5 e 6. As respostas obtidas para os Ids 3 e 6 podem ser vistas nas imagens abaixo:

![Resposta Id : 3](imagens\Buscar_3.png)
![Resposta Id : 6](imagens\Buscar_6.png)


Observando as respostas obtidas, concluí-se que o teste ocorreu corretamente. A resposta para venda de Id 3 resultou no retorno desse objeto, enquanto a resposta de Id 6 retornou um erro de NotFound, como se esperava.


---

:mag: **Teste de Criação de Venda**


- Para este teste foi tentando criar as seguintes vendas:
 - Sem Produtos;
 - Com Produtos sem nome;
 - Com Produtos sem id / id = 0;
 - Vendedor sem nome;
 - Vendedor sem id / id = 0;
 - Venda completa;


Os resultados, em ordem, se encontram a seguir:

![Resposta Venda Sem Produtos](imagens\Resgistro_Sem_Produtos.png)


![Resposta Venda Com Produtos sem nome](imagens\Registro_Produto_Nome.png)


![Resposta Venda Com Produtos sem id / id = 0](imagens\Registro_Produto_Id.png)


![Resposta Venda Vendedor sem nome](imagens\Registro_Vendedor_Nome.png)


![Resposta Venda Vendedor sem id / id = 0](imagens\Registro_Vendedor_Id.png)


![Resposta Venda completa](imagens\Registro_Completo.png)


Como visto, a tentativa de se cadastrar a venda com ausência de dados retorna um erro e o cadastro completo é feito corretamente.

---

:mag: **Teste de Atualização de Status**


- O teste consiste em tentar atualizar o status das vendas previamente cadastradas. As atualizações requistadas foram:
  - De: **Aguardando pagamento** Para: **Pagamento Aprovado**
  - De: **Aguardando pagamento** Para: **Cancelada**
  - De: **Pagamento Aprovado** Para: **Enviado para Transportadora**
  - De: **Pagamento Aprovado** Para: **Cancelada**
  - De: **Enviado para Transportador**. Para: **Entregue**
  - De: **Pagamento Aprovado** para Aguardando **Aguardando pagamento**
  - Tentado trocar o Status **Cancelada**.


As cinco primeiras requisições devem retornar sucesso, uma vez que é permitida a atualização. A penúltima requisição deve retornar o erro "O Status atual não pode ser mudado para o novo Status!" e a última deve retornar "O Status Atual não pode ser alterado!":


![Atualização 1](imagens\Atualizar_1.png)


![Atualização 2](imagens\Atualizar_2.png)


![Atualização 3](imagens\Atualizar_3.png)


![Atualização 4](imagens\Atualizar_4.png)


![Atualização 5](imagens\Atualizar_5.png)


![Atualização 6](imagens\Atualizar_6.png)


![Atualização 7](imagens\Atualizar_7.png)


- Teste ocorreu corretamente :white_check_mark: 
---

## INSTRUÇÕES PARA O TESTE TÉCNICO

- Crie um fork deste projeto (https://gitlab.com/Pottencial/tech-test-payment-api/-/forks/new). É preciso estar logado na sua conta Gitlab;
- Adicione @Pottencial (Pottencial Seguradora) como membro do seu fork. Você pode fazer isto em  https://gitlab.com/`your-user`/tech-test-payment-api/settings/members;
 - Quando você começar, faça um commit vazio com a mensagem "Iniciando o teste de tecnologia" e quando terminar, faça o commit com uma mensagem "Finalizado o teste de tecnologia";
 - Commit após cada ciclo de refatoração pelo menos;
 - Não use branches;
 - Você deve prover evidências suficientes de que sua solução está completa indicando, no mínimo, que ela funciona;

## O TESTE
- Construir uma API REST utilizando .Net Core, Java ou NodeJs (com Typescript);
- A API deve expor uma rota com documentação swagger (http://.../api-docs).
- A API deve possuir 3 operações:
  1) Registrar venda: Recebe os dados do vendedor + itens vendidos. Registra venda com status "Aguardando pagamento";
  2) Buscar venda: Busca pelo Id da venda;
  3) Atualizar venda: Permite que seja atualizado o status da venda.
     * OBS.: Possíveis status: `Pagamento aprovado` | `Enviado para transportadora` | `Entregue` | `Cancelada`.
- Uma venda contém informação sobre o vendedor que a efetivou, data, identificador do pedido e os itens que foram vendidos;
- O vendedor deve possuir id, cpf, nome, e-mail e telefone;
- A inclusão de uma venda deve possuir pelo menos 1 item;
- A atualização de status deve permitir somente as seguintes transições: 
  - De: `Aguardando pagamento` Para: `Pagamento Aprovado`
  - De: `Aguardando pagamento` Para: `Cancelada`
  - De: `Pagamento Aprovado` Para: `Enviado para Transportadora`
  - De: `Pagamento Aprovado` Para: `Cancelada`
  - De: `Enviado para Transportador`. Para: `Entregue`
- A API não precisa ter mecanismos de autenticação/autorização;
- A aplicação não precisa implementar os mecanismos de persistência em um banco de dados, eles podem ser persistidos "em memória".

## PONTOS QUE SERÃO AVALIADOS
- Arquitetura da aplicação - embora não existam muitos requisitos de negócio, iremos avaliar como o projeto foi estruturada, bem como camadas e suas responsabilidades;
- Programação orientada a objetos;
- Boas práticas e princípios como SOLID, DDD (opcional), DRY, KISS;
- Testes unitários;
- Uso correto do padrão REST;
