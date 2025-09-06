using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public class FilterPeopleFromXmlSolution
{
	public class PeopleStats
	{
		public List<string> Names { get; set; }
		public decimal TotalSalary { get; set; }
		public decimal AverageSalary { get; set; }
		public decimal MaxSalary { get; set; }
		public int Count { get; set; }
	}

	public static string FilterPeopleFromXml(string xmlData)
	{
		if (string.IsNullOrWhiteSpace(xmlData))
			return JsonSerializer.Serialize(new PeopleStats
			{
				Names = new List<string>(),
				TotalSalary = 0,
				AverageSalary = 0,
				MaxSalary = 0,
				Count = 0
			});

		var xdoc = XDocument.Parse(xmlData);

		var filteredPeople = xdoc.Descendants("Person")
			.Select(p => new
			{
				Name = p.Element("Name")?.Value,
				Age = int.Parse(p.Element("Age")?.Value ?? "0"),
				Department = p.Element("Department")?.Value,
				Salary = decimal.Parse(p.Element("Salary")?.Value ?? "0"),
				HireDate = DateTime.Parse(p.Element("HireDate")?.Value ?? DateTime.MinValue.ToString())
			})
			.Where(p =>
				p.Age > 30 &&
				string.Equals(p.Department, "IT", StringComparison.OrdinalIgnoreCase) &&
				p.Salary > 5000 &&
				p.HireDate.Year < 2019
			)
			.ToList();

		var names = filteredPeople.Select(p => p.Name).OrderBy(n => n).ToList();
		var totalSalary = filteredPeople.Sum(p => p.Salary);
		var maxSalary = filteredPeople.Any() ? filteredPeople.Max(p => p.Salary) : 0;
		var averageSalary = filteredPeople.Any() ? filteredPeople.Average(p => p.Salary) : 0;
		var count = filteredPeople.Count;

		var stats = new PeopleStats
		{
			Names = names,
			TotalSalary = totalSalary,
			MaxSalary = maxSalary,
			AverageSalary = averageSalary,
			Count = count
		};

		return JsonSerializer.Serialize(stats);
	}
}
