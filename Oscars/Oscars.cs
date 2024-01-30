using Oscars.Components.Pages;
using Oscars.Data;
using System.Data;

namespace Oscars
{
	public class Oscars
	{
		public static string ordinalSuffix(int num)
		{
			var ones = num % 10;
			var tens = Math.Floor(num / 10.0) % 10;
			if (tens == 1)
				return "th";
			else
				return ones switch
				{
					1 => "st",
					2 => "nd",
					3 => "rd",
					_ => "th"
				};
		}

	}
}
