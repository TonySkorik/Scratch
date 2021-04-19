using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
	public static class StringExtensions
	{
		public static void Dump(this string target)
		{
			Console.WriteLine(
				string.IsNullOrEmpty(target)
					? "NULL"
					: target);
		}
	}
}
