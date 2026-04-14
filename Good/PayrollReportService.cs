// ============================================================
// EXEMPLO BOM: Nomes que dispensam comentarios
// ============================================================
// O mesmo ReportService reescrito. Zero comentarios explicativos.
// Os nomes contam a historia sozinhos.

namespace MeaningfulNames.Good;

public class PayrollReportService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITimeTrackingService _timeTrackingService;

    private const double StandardMonthlyHours = 160;
    private const decimal OvertimeMultiplier = 1.5m;

    public PayrollReportService(
        IEmployeeRepository employeeRepository,
        ITimeTrackingService timeTrackingService)
    {
        _employeeRepository = employeeRepository;
        _timeTrackingService = timeTrackingService;
    }

    public List<EmployeePayrollSummary> GenerateByDepartmentAsync(
        DateTime periodStart,
        DateTime periodEnd,
        int departmentId)
    {
        var activeEmployees = _employeeRepository.GetActiveByDepartmentIdAsync(departmentId);
        var payrollSummaries = new List<EmployeePayrollSummary>();

        foreach (var employee in activeEmployees)
        {
            var hoursWorked = _timeTrackingService.GetHoursWorkedAsync(
                employee.Id, periodStart, periodEnd);

            var basePay = hoursWorked * employee.HourlyRate;
            var overtimePay = CalculateOvertimePay(hoursWorked, employee.HourlyRate);
            var totalPay = basePay + overtimePay;

            payrollSummaries.Add(new EmployeePayrollSummary(
                EmployeeName: employee.FullName,
                HoursWorked: hoursWorked,
                OvertimeHours: Math.Max(0, hoursWorked - StandardMonthlyHours),
                TotalPay: totalPay,
                Period: new DateRange(periodStart, periodEnd)));
        }

        return payrollSummaries;
    }

    private decimal CalculateOvertimePay(double hoursWorked, decimal hourlyRate)
    {
        if (hoursWorked <= StandardMonthlyHours)
            return 0;

        var overtimeHours = hoursWorked - StandardMonthlyHours;
        return (decimal)overtimeHours * hourlyRate * (OvertimeMultiplier - 1);
    }
}

public record EmployeePayrollSummary(
    string EmployeeName,
    double HoursWorked,
    double OvertimeHours,
    decimal TotalPay,
    DateRange Period);

public record DateRange(DateTime Start, DateTime End);

// Compare com a versao ruim:
//
//   RUIM                          BOM
//   Gen(d1, d2, dept)       ->    GenerateByDepartmentAsync(periodStart, periodEnd, departmentId)
//   var res = new List       ->    var payrollSummaries = new List
//   var emps = GetEmps       ->    var activeEmployees = GetActiveByDepartmentIdAsync
//   var h = GetH(e, d1, d2) ->    var hoursWorked = GetHoursWorkedAsync(...)
//   var tp = h * e.Rate      ->    var basePay = hoursWorked * employee.HourlyRate
//   var ot = h - 160         ->    var overtimeHours = hoursWorked - StandardMonthlyHours
//   n, hrs, val, d           ->    EmployeeName, HoursWorked, TotalPay, Period
//
// Nenhum comentario. Mesmo assim, voce entende tudo.
