// ============================================================
// DESAFIO 1.2 — Meaningful Names Over Comments
// ============================================================
// Este código depende totalmente de comentários para ser entendido.
// Sua missão: renomeie tudo para que os comentários se tornem desnecessários.
// Depois, remova todos os comentários que ficaram redundantes.
//
// Dicas:
// - Variáveis devem dizer o que representam
// - Métodos devem dizer o que fazem
// - Booleanos devem ter prefixo Is/Has/Can/Should
// - Constantes devem nomear o significado, não o valor
// - Mantenha apenas comentários que explicam "por quê"
//
// Crie o arquivo ChallengeSolved.cs com sua solução.

namespace MeaningfulNames.Challenge;

public class Svc
{
    private readonly IRepo _r;
    private const int MAX = 50;       // máximo de resultados por página
    private const double TAX = 0.15;  // taxa de imposto

    public Svc(IRepo r)
    {
        _r = r;
    }

    // Busca os itens ativos e calcula o valor total com imposto
    public async Task<Res> Calc(int uid, int p)
    {
        var u = await _r.GetU(uid);     // usuário
        if (u == null)
            return new Res { Ok = false };

        var lst = await _r.GetI(uid, p, MAX);  // itens da página
        var t = 0.0;  // total

        foreach (var i in lst)
        {
            if (i.A)  // se está ativo
            {
                var v = i.P * i.Q;    // valor = preço * quantidade
                var tx = v * TAX;     // imposto
                t += v + tx;          // acumula
            }
        }

        var d = 0.0;  // desconto
        if (u.Pr)     // se é premium
            d = t * 0.1;  // 10% de desconto

        return new Res
        {
            Ok = true,
            V = t - d,       // valor final
            D = d,           // desconto aplicado
            Cnt = lst.Count  // quantidade de itens
        };
    }

    // Verifica se o usuário pode comprar
    public bool Chk(User u, double v)
    {
        if (u == null) return false;
        if (!u.A) return false;          // se não está ativo
        if (u.B < v) return false;       // se saldo é menor que valor
        if (u.Bl) return false;          // se está bloqueado
        return true;
    }
}

public class Res
{
    public bool Ok { get; set; }
    public double V { get; set; }    // valor
    public double D { get; set; }    // desconto
    public int Cnt { get; set; }     // contagem
}
