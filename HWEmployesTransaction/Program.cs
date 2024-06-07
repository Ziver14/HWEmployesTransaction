namespace HWEmployesTransaction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание тестовых данных
            List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "John Doe", Department = "Sales", Salary = 5000, PositionLevel = "Manager" },
            new Employee { Name = "Jane Smith", Department = "Sales", Salary = 4500, PositionLevel = "Specialist" },
            new Employee { Name = "Bob Johnson", Department = "IT", Salary = 6000, PositionLevel = "Manager" },
            new Employee { Name = "Alice Williams", Department = "IT", Salary = 5500, PositionLevel = "Specialist" },
            new Employee { Name = "Tom Brown", Department = "HR", Salary = 4000, PositionLevel = "Specialist" },
            new Employee { Name = "Sarah Davis", Department = "HR", Salary = 4200, PositionLevel = "Specialist" }
        };

            // 1. Рассчитать среднюю зарплату для каждого отдела
            var averageSalaryByDepartment = employees.GroupBy(e => e.Department)
                                                    .Select(g => new { Department = g.Key, AverageSalary = g.Average(e => e.Salary) })
                                                    .OrderByDescending(d => d.AverageSalary);

            Console.WriteLine("Average salary by department:");
            foreach (var department in averageSalaryByDepartment)
            {
                Console.WriteLine($"{department.Department}: {department.AverageSalary:C}");
            }
            Console.WriteLine();

            // 2. Найти отдел с самой высокой средней зарплатой
            var departmentWithHighestAverageSalary = averageSalaryByDepartment.FirstOrDefault();
            Console.WriteLine($"Department with the highest average salary: {departmentWithHighestAverageSalary.Department} - {departmentWithHighestAverageSalary.AverageSalary:C}");
            Console.WriteLine();

            // 3. Найти всех работников с зарплатой выше средней по всем отделам
            var overallAverageSalary = employees.Average(e => e.Salary);
            var employeesAboveAverageSalary = employees.Where(e => e.Salary > overallAverageSalary);

            Console.WriteLine("Employees with salary above the overall average:");
            foreach (var employee in employeesAboveAverageSalary)
            {
                Console.WriteLine($"{employee.Name} - {employee.Salary:C}");
            }
            Console.WriteLine();

            // 4. Найти суммарную зарплату для каждого уровня должности
            var totalSalaryByPositionLevel = employees.GroupBy(e => e.PositionLevel)
                                                     .Select(g => new { PositionLevel = g.Key, TotalSalary = g.Sum(e => e.Salary) })
                                                     .OrderByDescending(p => p.TotalSalary);

            Console.WriteLine("Total salary by position level:");
            foreach (var positionLevel in totalSalaryByPositionLevel)
            {
                Console.WriteLine($"{positionLevel.PositionLevel}: {positionLevel.TotalSalary:C}");

            }
            Console.WriteLine();
            //Транзакции
            var transactions = new List<Transaction>
            {
                 new Transaction { TransactionId = "1", Amount = 100.0m, Date = new DateTime(2023, 4, 1) },
                 new Transaction { TransactionId = "2", Amount = 200.0m, Date = new DateTime(2023, 4, 2) },
                 new Transaction { TransactionId = "3", Amount = 150.0m, Date = new DateTime(2023, 4, 3) },
                 new Transaction { TransactionId = "4", Amount = 75.0m, Date = new DateTime(2023, 4, 4) },
                 new Transaction { TransactionId = "5", Amount = 125.0m, Date = new DateTime(2023, 4, 5) }
            };

            var startDate = new DateTime(2023, 4, 1);
            var endDate = new DateTime(2023, 4, 5);
            var averageAmount = GetAverageTransactionAmount(transactions, startDate, endDate);
            Console.WriteLine($"Average transaction amount: {averageAmount:C}");

        }
        public static decimal GetAverageTransactionAmount<T>(IEnumerable<T> transactions, DateTime startDate, DateTime endDate)
        where T : ITransaction
        {
            var filteredTransactions = transactions.Where(t => t.Date >= startDate && t.Date <= endDate);
            var averageAmount = filteredTransactions.Average(t => t.Amount);
            return averageAmount;
        }
    }
}
