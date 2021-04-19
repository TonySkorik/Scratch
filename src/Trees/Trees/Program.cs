using System;
using Trees.Nodes;

namespace Trees
{
	class Program
	{
		static void Main(string[] args)
		{
			BinaryTree<int, string> testBinaryTree = new(10, "10");

			testBinaryTree.AddOrUpdate(11, "11");
			testBinaryTree.AddOrUpdate(9,"9");
			testBinaryTree.AddOrUpdate(5,"5");

			testBinaryTree.Find(11).Dump();
			testBinaryTree.Find(9).Dump();
			testBinaryTree.Find(5).Dump();

			testBinaryTree.AddOrUpdate(8, "8");
			testBinaryTree.Remove(9);

			testBinaryTree.Find(8).Dump();
			testBinaryTree.Find(9).Dump();
			testBinaryTree.Find(5).Dump();

			Console.ReadKey();
			Console.WriteLine("Press any key to exit...");
		}
	}
}
