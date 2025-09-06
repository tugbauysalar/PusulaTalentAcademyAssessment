using System;
using System.Collections.Generic;
using System.Text.Json;

public class MaxIncreasingSubArrayAsJsonSolution
{
	/// <summary>
	/// Verilen sayı listesinde ardışık artan alt dizilerden
	/// toplamı en yüksek olan alt diziyi bulur ve JSON olarak döndürür.
	/// </summary>
	public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
	{
		if (numbers == null || numbers.Count == 0)
			return JsonSerializer.Serialize(new List<int>());

		var currentSubArray = new List<int>();
		var maxSubArray = new List<int>();
		int currentSum = 0;
		int maxSum = int.MinValue;

		foreach (var num in numbers)
		{
			if (currentSubArray.Count == 0 || num > currentSubArray[^1])
			{
				currentSubArray.Add(num);
				currentSum += num;
			}
			else
			{
				if (currentSum > maxSum)
				{
					maxSum = currentSum;
					maxSubArray = new List<int>(currentSubArray);
				}

				currentSubArray.Clear();
				currentSubArray.Add(num);
				currentSum = num;
			}
		}

		if (currentSum > maxSum)
			maxSubArray = new List<int>(currentSubArray);

		return JsonSerializer.Serialize(maxSubArray);
	}
}
