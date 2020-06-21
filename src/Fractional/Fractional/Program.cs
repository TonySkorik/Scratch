using System;

namespace Fractional
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Test Addition");
			Console.WriteLine(new string('-', 10));
			TestAdd();

			Console.WriteLine("Test Subtraction");
			Console.WriteLine(new string('-', 10));
			TestSubtract();

			Console.WriteLine("Test Product");
			Console.WriteLine(new string('-', 10));
			TestProduct();

			Console.WriteLine("Test Comparison");
			Console.WriteLine(new string('-', 10));
			TestCompare();

			Console.ReadKey();
		}

		private static void TestCompare()
		{
			Fractional f1 = new Fractional("2");
			Fractional f2 = new Fractional("2");
			var comparisonResult = f1.CompareTo(f2);
			var sign = comparisonResult == 0
				? "="
				: comparisonResult < 0
					? "<"
					: ">";
			Console.WriteLine($"{f1}{sign}{f2}");
			
			f1 = new Fractional("3.1");
			f2 = new Fractional("2.53");
			comparisonResult = f1.CompareTo(f2);
			sign = comparisonResult == 0
				? "="
				: comparisonResult < 0
					? "<"
					: ">";
			Console.WriteLine($"{f1}{sign}{f2}");
		}

		private static void TestProduct()
		{
			Fractional f1 = new Fractional("2");
			Fractional f2 = new Fractional("2");
			var product = f1.Multiply(f2);
			Console.WriteLine($"{f1}*{f2}={product}");

			f1 = new Fractional("2");
			f2 = new Fractional("0");
			product = f1.Multiply(f2);
			Console.WriteLine($"{f1}*{f2}={product}");

			f1 = new Fractional("-2");
			f2 = new Fractional("1");
			product = f1.Multiply(f2);
			Console.WriteLine($"{f1}*{f2}={product}");
			
			f1 = new Fractional("-1567");
			f2 = new Fractional("1567");
			product = f1.Multiply(f2);
			Console.WriteLine($"{f1}*{f2}={product}");
		}

		private static void TestSubtract()
		{
			Fractional f1 = new Fractional("1.9");
			Fractional f2 = new Fractional("1.1");
			var remainder = f1.Subtract(f2);
			Console.WriteLine($"{f1}-{f2}={remainder}");

			f1 = new Fractional("1.1");
			f2 = new Fractional("1.9");
			remainder = f1.Subtract(f2);
			Console.WriteLine($"{f1}-{f2}={remainder}");
		}

		private static void TestAdd()
		{
			var f1 = new Fractional("1.0");
			var f2 = new Fractional("-1.15");
			var addition = f1.Add(f2);
			Console.WriteLine($"{f1}+{f2}={addition}");

			f1 = new Fractional("-0,1");
			f2 = new Fractional("1,15");
			addition = f1.Add(f2);
			Console.WriteLine($"{f1}+{f2}={addition}");

			f1 = new Fractional("-1.05");
			f2 = new Fractional("1.2");
			addition = f1.Add(f2);
			Console.WriteLine($"{f1}+{f2}={addition}");

			f1 = new Fractional("1.2");
			f2 = new Fractional("-1.05");
			addition = f1.Add(f2);
			Console.WriteLine($"{f1}+{f2}={addition}");

			f1 = new Fractional("1.9");
			f2 = new Fractional("1.1");
			addition = f1.Add(f2);
			Console.WriteLine($"{f1}+{f2}={addition}");

			f1 = new Fractional("1.38");
			f2 = new Fractional("1.1567");
			addition = f1.Add(f2);
			Console.WriteLine($"{f1}+{f2}={addition}");

			f1 = new Fractional("1.0");
			f2 = new Fractional("1.15");
			addition = f1.Add(f2);
			Console.WriteLine($"{f1}+{f2}={addition}");
			
			f1 = new Fractional("0.0");
			f2 = new Fractional("1.15");
			addition = f1.Add(f2);
			Console.WriteLine($"{f1}+{f2}={addition}");
		}
	}
}
