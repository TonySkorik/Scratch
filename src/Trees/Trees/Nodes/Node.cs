using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees.Nodes
{
	internal class Node<TKey, TValue>
	{
		public TKey Key { set; get; }
		public TValue Value { set; get; }
		
		public Node(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public virtual bool IsLeaf()
		{
			return false;
		}
	}
}
