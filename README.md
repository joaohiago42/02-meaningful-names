# 1.2 Meaningful Names Over Comments (Nomes Significativos > Comentarios)

> **Fase 1 — Escrevendo Codigo Limpo** | Roadmap: Software Design & Architecture

## O Conceito

Se voce precisa de um comentario para explicar o que uma variavel, metodo ou classe faz, **o nome dela esta ruim**. Um bom nome torna o comentario desnecessario.

**Por que comentarios sao perigosos?** Comentarios mentem. O codigo muda, mas o comentario fica la, desatualizado. Um nome ruim com comentario correto hoje vira nome ruim com comentario errado amanha.

**Analogia:** Duas portas num corredor:

- **Porta 1:** Porta branca com post-it: _"Sala de Reunioes"_
- **Porta 2:** Placa gravada: **"Sala de Reunioes"**

Post-its caem, desatualizam, somem. Placas gravadas fazem parte da porta — se a sala mudar de funcao, a placa velha incomoda e alguem troca. **Comentarios = post-its. Bons nomes = placas gravadas.**

## Regras Praticas para Bons Nomes

### Variaveis

| Ruim | Bom | Por que |
|------|-----|---------|
| `d` | `daysSinceCreation` | Sem comentario, voce sabe o que e |
| `tp` | `totalPay` | Dispensa `// total a pagar` |
| `res` | `payrollSummaries` | O tipo e o proposito ficam claros |
| `e` | `employee` | Ninguem precisa decifrar |
| `dt` | `periodStart` | Diz o que a data representa |

### Metodos

| Ruim | Bom | Por que |
|------|-----|---------|
| `Gen()` | `GenerateByDepartmentAsync()` | Gerar o que? De onde? |
| `Process()` | `MarkAsCompleted()` | Diz exatamente o que acontece |
| `Check()` | `HasElevatedPermissions()` | O retorno bool fica obvio |
| `GetPrice()` | `ApplyFifteenPercentDiscount()` | Se o desconto mudar, o nome incomoda |
| `GetH()` | `GetHoursWorkedAsync()` | Horas de que? Trabalhadas. |

### Booleanos — prefixe com Is, Has, Can, Should

| Ruim | Bom |
|------|-----|
| `flag` | `isExpired` |
| `check` | `hasPermission` |
| `converted` | `isConverted` |
| `ok` | `canProceed` |

### Constantes — nomeie o significado, nao o valor

| Ruim | Bom |
|------|-----|
| `160` | `StandardMonthlyHours` |
| `0.5` | `OvertimeMultiplier` |
| `"Admin"` | `Role.Admin` (enum) |

## Quando Comentarios SAO Uteis

Nomes substituem comentarios **sobre o que**. Mas existem comentarios validos:

- **Por que** uma decisao foi tomada: `// Usamos cache aqui porque a API externa tem rate limit de 100 req/min`
- **Avisos**: `// IMPORTANTE: esta ordem de chamadas importa por causa do lock no banco`
- **TODOs temporarios**: `// TODO: remover apos migrar para v2 (ticket #1234)`
- **Regex complexas**: `// Formato: DD/MM/YYYY HH:mm`

A regra nao e "nunca comente". E: **nao use comentarios para compensar nomes ruins**.

## Estrutura do Projeto

```
02-meaningful-names/
  Bad/
    ReportService.cs      # Sopa de letrinhas: d1, d2, res, tp, ot, e
    OrderProcessor.cs     # Comentarios que mentem e ficam desatualizados
  Good/
    PayrollReportService.cs   # Mesmo servico, nomes auto-explicativos
    OrderProcessor.cs         # Mesmo servico, nomes que nao mentem
  README.md
```

## Exemplos Reais do Codigo de Trabalho (Aiko)

### Bom: Booleanos com prefixo `Is`

`LowDataMessagePersistence.cs`:

```csharp
public bool IsConverted { get; set; }
public bool IsSkipped { get; set; }
public bool IsReprocessed { get; set; }
```

Zero ambiguidade. O prefixo `Is` deixa claro que e booleano e o que ele representa.

### Bom: Metodos auto-explicativos

`Task.cs`:

```csharp
public DateTime GetStartTime()
public DateTime GetEndTime()
```

### Ruim: Abreviacoes cripticas

`MessageBatchProcessor.cs`:

```csharp
foreach (var msg in batch) {
    msg.Completion.TrySetResult(true);
}
```

`msg` poderia ser `bufferedMessage` ou `queuedMessage` — sem abreviacao, o tipo fica claro.

`ReceivedIccidEventHandlerTest.cs`:

```csharp
var evt = new ReceivedIccidEvent(1, "1234567890", DateTime.Now);
```

`evt` poderia ser `receivedIccidEvent` — o nome completo custa 15 caracteres e economiza 15 segundos de quem le.

### Ruim: Comentario TODO que deveria ser um nome melhor

`EquipmentModel.cs`:

```csharp
// TODO: REMOVE 'PERCENTAGE' FROM NAME
public double CorrectiveMaintenancePercentage { get; private set; }
```

O TODO esta la ha meses. Se o nome estivesse correto desde o inicio, o comentario nao existiria.

## Regra de Ouro

> **Se voce precisa de um comentario para explicar O QUE o codigo faz, renomeie.**
> **Se voce precisa de um comentario para explicar POR QUE o codigo faz, mantenha.**

## Checklist

- [ ] Variaveis tem nomes completos e descritivos?
- [ ] Metodos usam verbos que descrevem a acao?
- [ ] Booleanos usam prefixo Is/Has/Can/Should?
- [ ] Numeros magicos foram extraidos para constantes nomeadas?
- [ ] Comentarios explicam "por que", nao "o que"?
- [ ] Nao existem comentarios compensando nomes ruins?

## Referencia

- Clean Code (Robert C. Martin) — Cap. 2: Meaningful Names, Cap. 4: Comments
- [Roadmap.sh — Clean Code](https://roadmap.sh/software-design-architecture)
