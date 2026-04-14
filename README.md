# 1.2 Meaningful Names Over Comments (Nomes Significativos > Comentários)

> **Fase 1 — Escrevendo Código Limpo** | Roadmap: Software Design & Architecture

## O Conceito

Se você precisa de um comentário para explicar o que uma variável, método ou classe faz, **o nome dela está ruim**. Um bom nome torna o comentário desnecessário.

**Por que comentários são perigosos?** Comentários mentem. O código muda, mas o comentário fica lá, desatualizado. Um nome ruim com comentário correto hoje vira nome ruim com comentário errado amanhã.

**Analogia:** Duas portas num corredor:

- **Porta 1:** Porta branca com post-it: _"Sala de Reuniões"_
- **Porta 2:** Placa gravada: **"Sala de Reuniões"**

Post-its caem, desatualizam, somem. Placas gravadas fazem parte da porta — se a sala mudar de função, a placa velha incomoda e alguém troca. **Comentários = post-its. Bons nomes = placas gravadas.**

## Regras Práticas para Bons Nomes

### Variáveis

| Ruim | Bom | Por quê |
|------|-----|---------|
| `d` | `daysSinceCreation` | Sem comentário, você sabe o que é |
| `tp` | `totalPay` | Dispensa `// total a pagar` |
| `res` | `payrollSummaries` | O tipo e o propósito ficam claros |
| `e` | `employee` | Ninguém precisa decifrar |
| `dt` | `periodStart` | Diz o que a data representa |

### Métodos

| Ruim | Bom | Por quê |
|------|-----|---------|
| `Gen()` | `GenerateByDepartmentAsync()` | Gerar o quê? De onde? |
| `Process()` | `MarkAsCompleted()` | Diz exatamente o que acontece |
| `Check()` | `HasElevatedPermissions()` | O retorno bool fica óbvio |
| `GetPrice()` | `ApplyFifteenPercentDiscount()` | Se o desconto mudar, o nome incomoda |
| `GetH()` | `GetHoursWorkedAsync()` | Horas de quê? Trabalhadas. |

### Booleanos — prefixe com Is, Has, Can, Should

| Ruim | Bom |
|------|-----|
| `flag` | `isExpired` |
| `check` | `hasPermission` |
| `converted` | `isConverted` |
| `ok` | `canProceed` |

### Constantes — nomeie o significado, não o valor

| Ruim | Bom |
|------|-----|
| `160` | `StandardMonthlyHours` |
| `0.5` | `OvertimeMultiplier` |
| `"Admin"` | `Role.Admin` (enum) |

## Quando Comentários SÃO Úteis

Nomes substituem comentários **sobre o quê**. Mas existem comentários válidos:

- **Por quê** uma decisão foi tomada: `// Usamos cache aqui porque a API externa tem rate limit de 100 req/min`
- **Avisos**: `// IMPORTANTE: esta ordem de chamadas importa por causa do lock no banco`
- **TODOs temporários**: `// TODO: remover após migrar para v2 (ticket #1234)`
- **Regex complexas**: `// Formato: DD/MM/YYYY HH:mm`

A regra não é "nunca comente". É: **não use comentários para compensar nomes ruins**.

## Estrutura do Projeto

```text
02-meaningful-names/
  Bad/
    ReportService.cs      # Sopa de letrinhas: d1, d2, res, tp, ot, e
    OrderProcessor.cs     # Comentários que mentem e ficam desatualizados
  Good/
    PayrollReportService.cs   # Mesmo serviço, nomes auto-explicativos
    OrderProcessor.cs         # Mesmo serviço, nomes que não mentem
  README.md
```

## Exemplos Reais do Código de Trabalho (Aiko)

### Bom: Booleanos com prefixo `Is`

`LowDataMessagePersistence.cs`:

```csharp
public bool IsConverted { get; set; }
public bool IsSkipped { get; set; }
public bool IsReprocessed { get; set; }
```

Zero ambiguidade. O prefixo `Is` deixa claro que é booleano e o que ele representa.

### Bom: Métodos auto-explicativos

`Task.cs`:

```csharp
public DateTime GetStartTime()
public DateTime GetEndTime()
```

### Ruim: Abreviações crípticas

`MessageBatchProcessor.cs`:

```csharp
foreach (var msg in batch) {
    msg.Completion.TrySetResult(true);
}
```

`msg` poderia ser `bufferedMessage` ou `queuedMessage` — sem abreviação, o tipo fica claro.

`ReceivedIccidEventHandlerTest.cs`:

```csharp
var evt = new ReceivedIccidEvent(1, "1234567890", DateTime.Now);
```

`evt` poderia ser `receivedIccidEvent` — o nome completo custa 15 caracteres e economiza 15 segundos de quem lê.

### Ruim: Comentário TODO que deveria ser um nome melhor

`EquipmentModel.cs`:

```csharp
// TODO: REMOVE 'PERCENTAGE' FROM NAME
public double CorrectiveMaintenancePercentage { get; private set; }
```

O TODO está lá há meses. Se o nome estivesse correto desde o início, o comentário não existiria.

## Regra de Ouro

> **Se você precisa de um comentário para explicar O QUE o código faz, renomeie.**
> **Se você precisa de um comentário para explicar POR QUE o código faz, mantenha.**

## Checklist

- [ ] Variáveis têm nomes completos e descritivos?
- [ ] Métodos usam verbos que descrevem a ação?
- [ ] Booleanos usam prefixo Is/Has/Can/Should?
- [ ] Números mágicos foram extraídos para constantes nomeadas?
- [ ] Comentários explicam "por quê", não "o quê"?
- [ ] Não existem comentários compensando nomes ruins?

## Referência

- Clean Code (Robert C. Martin) — Cap. 2: Meaningful Names, Cap. 4: Comments
- [Roadmap.sh — Clean Code](https://roadmap.sh/software-design-architecture)
