using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class LongestVowelSubsequenceSolution
{
	private static readonly HashSet<char> Vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

	public class WordVowelInfo
	{
		public string word { get; set; }
		public string sequence { get; set; }
		public int length { get; set; }
	}

	/// <summary>
	/// Her kelimenin ardışık sesli harflerden oluşan en uzun alt dizisini bulur
	/// ve JSON formatında döndürür.
	/// </summary>
	public static string LongestVowelSubsequenceAsJson(List<string> words)
	{
		if (words == null || words.Count == 0)
			return JsonSerializer.Serialize(new List<WordVowelInfo>());

		var result = new List<WordVowelInfo>();

		foreach (var word in words)
		{
			string maxSeq = "";
			string currentSeq = "";

			foreach (var ch in word.ToLower())
			{
				if (Vowels.Contains(ch))
				{
					currentSeq += ch;
					if (currentSeq.Length > maxSeq.Length)
						maxSeq = currentSeq;
				}
				else
				{
					currentSeq = "";
				}
			}

			result.Add(new WordVowelInfo
			{
				word = word,
				sequence = maxSeq,
				length = maxSeq.Length
			});
		}

		return JsonSerializer.Serialize(result);
	}
}
