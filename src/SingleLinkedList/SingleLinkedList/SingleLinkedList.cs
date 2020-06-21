using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SingleLinkedList
{
	public class SingleLinkedList : IEnumerable<int>
	{
		#region Nested classes

		private class Node
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

		#endregion

		public class SingleLinkedListEnumerator : IEnumerator<int>
		{
			private readonly SingleLinkedList _list;
			private Node _currentNode;
			private int _currentValue;

			public int Current => _currentValue;
			object IEnumerator.Current => Current;

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

		public IEnumerator<int> GetEnumerator()
		{ 
			return new SingleLinkedListEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
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
