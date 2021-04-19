using System;

namespace Trees.Nodes
{
	internal class BTreeNode<TKey, TValue> : Node<TKey, TValue>
		where TKey : IComparable<TKey>
	{
		public BTreeNode<TKey, TValue> LeftDescendant { set; get; }
		public BTreeNode<TKey, TValue> RightDescendant { set; get; }
		public BTreeNode<TKey, TValue> Parent { set; get; }

		public override bool IsLeaf() 
			=> LeftDescendant == null && RightDescendant == null;

		public BTreeNode(TKey key, TValue value) : base(key, value)
		{ }
	}
}
