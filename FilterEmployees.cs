using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class FilterEmployeesSolution
{
	public class EmployeeStats
	{
		public List<string> Names { get; set; }
		public decimal TotalSalary { get; set; }
		public decimal AverageSalary { get; set; }
		public decimal MinSalary { get; set; }
		public decimal MaxSalary { get; set; }
		public int Count { get; set; }
	}

	/// <summary>
	/// Verilen çalışan listesi üzerinde filtreleme ve istatistik hesaplaması yapar
	/// ve JSON olarak döndürür.
	/// </summary>
	public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
	{
		if (employees == null)
			return JsonSerializer.Serialize(new EmployeeStats
			{
				Names = new List<string>(),
				TotalSalary = 0,
				AverageSalary = 0,
				MinSalary = 0,
				MaxSalary = 0,
				Count = 0
			});

		var filtered = employees
			.Where(e =>
				e.Age >= 25 && e.Age <= 40 &&
				(string.Equals(e.Department, "IT", StringComparison.OrdinalIgnoreCase) ||
				 string.Equals(e.Department, "Finance", StringComparison.OrdinalIgnoreCase)) &&
				e.Salary >= 5000 && e.Salary <= 9000 &&
				e.HireDate.Year > 2017
			)
			.ToList();

		var names = filtered
			.OrderByDescending(e => e.Name.Length)
			.ThenBy(e => e.Name)
			.Select(e => e.Name)
			.ToList();

		var totalSalary = filtered.Sum(e => e.Salary);
		var minSalary = filtered.Any() ? filtered.Min(e => e.Salary) : 0;
		var maxSalary = filtered.Any() ? filtered.Max(e => e.Salary) : 0;
		var averageSalary = filtered.Any() ? Math.Round(filtered.Average(e => e.Salary), 2) : 0;
		var count = filtered.Count;

		var stats = new EmployeeStats
		{
			Names = names,
			TotalSalary = totalSalary,
			MinSalary = minSalary,
			MaxSalary = maxSalary,
			AverageSalary = averageSalary,
			Count = count
		};

		return JsonSerializer.Serialize(stats);
	}
}
