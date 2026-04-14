// ============================================================
// EXEMPLO RUIM: Comentarios que mentem / ficam desatualizados
// ============================================================
// Problemas:
// 1. Comentarios dizem uma coisa, codigo faz outra
// 2. Nomes vagos forcam a existencia dos comentarios
// 3. Comentarios que eram verdade quando foram escritos, mas o codigo mudou

namespace MeaningfulNames.Bad;

public class OrderProcessor
{
    // Processa o pedido e envia email de confirmacao
    public void Process(Order order)
    {
        order.Status = "Completed";
        _repository.Save(order);

        // MENTIRA: o email foi removido ha 6 meses, mas o comentario ficou.
        // Quem le acha que o email esta sendo enviado.
    }

    // Retorna o preco com desconto de 10%
    public decimal GetPrice(Order order)
    {
        // O desconto mudou pra 15% ha 3 meses.
        // Ninguem atualizou o comentario.
        return order.Total * 0.85m;
    }

    // Valida se o usuario tem permissao de admin
    public bool Check(User user)
    {
        // Comecou validando admin, depois adicionaram manager e supervisor.
        // O comentario so menciona admin.
        return user.Role == "Admin"
            || user.Role == "Manager"
            || user.Role == "Supervisor";
    }

    // TODO: REMOVER CAMPO 'PERCENTAGE' DO NOME
    // (exemplo real do trabalho: CorrectiveMaintenancePercentage)
    public double CorrectiveMaintenancePercentage { get; set; }
    // O nome esta errado e o TODO esta la ha meses. Um bom nome
    // eliminaria o TODO e a confusao.
}

// Moral da historia:
// - Comentarios nao tem compilador. Ninguem te avisa quando mentem.
// - Codigo muda, comentario fica. O comentario vira armadilha.
// - Se o nome fosse bom, o comentario nao existiria — e nao teria como mentir.
