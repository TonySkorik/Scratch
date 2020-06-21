using System;
using System.Collections;
using System.Collections.Generic;

namespace SingleLinkedList
{
	class Program
	{
		static void Main(string[] args)
		{
			var myList = new SingleLinkedList();
			myList.Add(1);
			myList.Add(42);
			myList.Add(1567);

			while (myList.MoveNext())
			{
				Console.WriteLine(myList.Current);
			}

			Console.WriteLine("With enumerator.");
			foreach (int value in myList)
			{
				Console.WriteLine(value);
			}

			Console.WriteLine("Done iterating.");
			Console.ReadKey();
		}
	}

	public class SingleLinkedList
	{
		public class Node
		{
			public int Value { get; }
			public Node Next { private set; get; }

			private Node(int value)
			{
				Value = value;
			}

			public static Node Create(int value)
			{
				return new Node(value);
			}

			public void SetNext(Node next)
			{
				Next = next;
			}
		}

		public class SingleLinkedListEnumerator : IEnumerator<int>
		{
			private readonly SingleLinkedList _list;
			private SingleLinkedList.Node _currentNode;
			private int _currentValue;

			public int Current => _currentValue;
			object? IEnumerator.Current => Current;
			
			public SingleLinkedListEnumerator(SingleLinkedList list)
			{
				_list = list;
			}

			public bool MoveNext()
			{
				if (_list._tail == null)
				{
					return false;
				}

				if (_currentNode == null)
				{
					_currentNode = _list._tail;
					_currentValue = _currentNode.Value;
					return true;
				}

				if (_currentNode.Next == null)
				{
					return false;
				}

				_currentNode = _currentNode.Next;
				_currentValue = _currentNode.Value;
				return true;
			}
			
			public void Reset()
			{
				return;
			}

			public void Dispose()
			{
				return;
			}
		}

		private Node _tail;
		private Node _head;
		private Node _currentNode;

		public int Current { private set; get; }

		public IEnumerator<int> GetEnumerator()
		{
			return new SingleLinkedListEnumerator(this);
		}
		
		public bool MoveNext()
		{
			if (_tail == null)
			{
				return false;
			}

			if (_currentNode == null)
			{
				_currentNode = _tail;
				Current = _currentNode.Value;
				return true;
			}
			
			if (_currentNode.Next == null)
			{
				return false;
			}

			_currentNode = _currentNode.Next;
			Current = _currentNode.Value;
			return true;
		}

		public void Add(int element)
		{
			var newNode = Node.Create(element);

			if (_tail == null)
			{
				_tail = newNode;
				_head = newNode;
				return;
			}
			
			_head.SetNext(newNode);
			_head = newNode;
		}
	}
}
