using System;
using System.Collections;
using System.Collections.Generic;

namespace SingleLinkedList
{
	class Program
	{
		static void Main(string[] args)
		{
			LinkedListTest();
			
			Console.ReadKey();
		}

		private static void LinkedListTest()
		{
			var myList = new SingleLinkedList();
			myList.Add(1);
			myList.Add(42);
			myList.Add(1567);

			//Console.WriteLine("First simple enumeration.");
			//while (myList.MoveNext())
			//{
			//	Console.WriteLine(myList.Current);
			//}

			//Console.WriteLine("Second simple enumeration.");
			//while (myList.MoveNext())
			//{
			//	Console.WriteLine(myList.Current);
			//}

			Console.WriteLine("First with enumerator.");
			foreach (int value in myList)
			{
				Console.WriteLine(value);
			}

			Console.WriteLine("Second with enumerator.");
			foreach (int value in myList)
			{
				Console.WriteLine(value);
			}

			Console.WriteLine("Done iterating.");
		}
	}
}
