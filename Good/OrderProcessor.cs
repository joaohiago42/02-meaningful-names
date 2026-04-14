// ============================================================
// EXEMPLO BOM: Nomes que nao podem mentir
// ============================================================
// O mesmo OrderProcessor reescrito. Quando o comportamento muda,
// o nome desatualizado incomoda — diferente do comentario, que ninguem le.

namespace MeaningfulNames.Good;

public class OrderCompletionService
{
    private readonly IOrderRepository _orderRepository;

    public OrderCompletionService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public void MarkAsCompleted(Order order)
    {
        order.Status = OrderStatus.Completed;
        _orderRepository.SaveAsync(order);
    }

    public decimal ApplyFifteenPercentDiscount(decimal originalPrice)
    {
        return originalPrice * 0.85m;
    }

    public bool HasElevatedPermissions(User user)
    {
        return user.Role is "Admin" or "Manager" or "Supervisor";
    }
}

// Compare com a versao ruim:
//
//   RUIM                                        BOM
//   Process(order)                          ->  MarkAsCompleted(order)
//   // Processa pedido e envia email             (sem comentario — o nome diz tudo)
//
//   GetPrice(order)                         ->  ApplyFifteenPercentDiscount(price)
//   // Retorna preco com desconto de 10%         (se mudar pra 20%, o nome incomoda e alguem corrige)
//
//   Check(user)                             ->  HasElevatedPermissions(user)
//   // Valida se tem permissao de admin          (o nome ja diz que nao e so admin)
//
// Regras:
// 1. Se o metodo faz UMA coisa, o nome consegue descrever essa coisa.
// 2. Se voce nao consegue dar um bom nome, talvez o metodo faca coisas demais.
// 3. Booleans: prefixe com Is, Has, Can, Should (ex: IsExpired, HasPermission).
// 4. Metodos: use verbos que dizem O QUE acontece (MarkAsCompleted, não Process).
