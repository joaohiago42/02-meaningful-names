// ============================================================
// EXEMPLO RUIM: Nomes que precisam de comentarios para fazer sentido
// ============================================================
// Problemas:
// 1. Variaveis com nomes cripticos (d, tp, res, val)
// 2. Metodos com nomes vagos que dependem de comentarios
// 3. Abreviacoes que forcam o leitor a decifrar

namespace MeaningfulNames.Bad;

public class ReportService
{
    // Gera relatorio de produtividade dos funcionarios
    public List<object> Gen(DateTime d1, DateTime d2, int dept)
    {
        var res = new List<object>();  // resultado
        var emps = GetEmps(dept);      // funcionarios

        foreach (var e in emps)
        {
            var h = GetH(e, d1, d2);  // horas trabalhadas
            var tp = h * e.Rate;       // total a pagar

            // Verifica se passou do limite
            if (h > 160)
            {
                var ot = h - 160;      // horas extras
                tp += ot * e.Rate * 0.5; // adicional de 50%
            }

            var obj = new              // ???
            {
                n = e.Name,            // nome
                hrs = h,               // horas
                val = tp,              // valor
                d = d2 - d1            // periodo
            };

            res.Add(obj);
        }

        return res;
    }

    // Busca os funcionarios
    private List<Employee> GetEmps(int d)
    {
        // d = departamento
        return _repository.GetByDept(d);
    }

    // Calcula horas
    private double GetH(Employee e, DateTime d1, DateTime d2)
    {
        // e = funcionario, d1 = inicio, d2 = fim
        return _timeTracker.Calculate(e.Id, d1, d2);
    }
}

// Problemas:
// - "Gen" -> gerar o que? Precisou do comentario acima.
// - "d1", "d2" -> inicio e fim? criacao e expiracao? ninguem sabe sem comentario.
// - "res", "emps", "e", "h", "tp", "ot", "obj", "val" -> sopa de letrinhas
// - "GetEmps" -> busca empregados de onde? todos? ativos?
// - "GetH" -> horas de que? trabalhadas? extras? disponiveis?
// - Cada linha precisa de um comentario ao lado pra fazer sentido.
//   Se os comentarios sumirem, o codigo vira enigma.
