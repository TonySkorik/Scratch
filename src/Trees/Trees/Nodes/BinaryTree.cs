using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees.Nodes
{
	internal class BinaryTree<TKey, TValue> : BTreeNode<TKey, TValue> 
		where  TKey : IComparable<TKey>
	{
		public BTreeNode<TKey, TValue> Root => (BTreeNode<TKey, TValue>)this;

		public BinaryTree(TKey rootKey, TValue rootValue) : base(rootKey, rootValue)
		{ }

		public void AddOrUpdate(TKey nodeKey, TValue nodeValue)
		{
			var root = Root;
			BTreeNode<TKey, TValue> parentNode = null;

			while(root != null)
			{
				var comparisonResult = nodeKey.CompareTo(root.Key);

				if (comparisonResult == 0)
				{
					// means equal keys
					Value = nodeValue;
					return;
				}

				parentNode = root;

				if (comparisonResult < 0)
				{
					// nodeKey < Key
					root = root.LeftDescendant;
				}

				if (comparisonResult > 0)
				{
					// nodeKey > Key
					root = root.RightDescendant;
				}
			}
			
			if (parentNode == null)
			{
				throw new InvalidOperationException("Invalid state!");
			}

			BTreeNode<TKey, TValue> nodeToInsert = new(nodeKey, nodeValue);
			nodeToInsert.Parent = parentNode;

			if (nodeToInsert.Key.CompareTo(parentNode.Key) < 0)
			{
				// nodeKey < parentNodeKey
				parentNode.LeftDescendant = nodeToInsert;
			}
			else
			{
				// nodeKey > parentNodeKey
				parentNode.RightDescendant = nodeToInsert;
			}
		}

		public TValue Find(TKey key)
		{
			var node = FindNode(key);
			return node == null
				? default
				: node.Value;
		}

		public void Remove(TKey key)
		{
			var nodeFindResult = FindNodeWithParent(key);

			if (nodeFindResult.node == null)
			{
				return;
			}

			if (nodeFindResult.node.IsLeaf())
			{
				if (nodeFindResult.isNodeRightDescendant)
				{
					nodeFindResult.parentNode.RightDescendant = null;
				}
				else
				{
					nodeFindResult.parentNode.LeftDescendant = null;
				}

				return;
			}
			
			if (nodeFindResult.node.RightDescendant == null)
			{
				if (nodeFindResult.isNodeRightDescendant)
				{
					nodeFindResult.parentNode.RightDescendant = nodeFindResult.node.LeftDescendant;
				}
				else
				{
					nodeFindResult.parentNode.LeftDescendant = nodeFindResult.node.LeftDescendant;
				}

				return;
			}

			// means right descendant is not null
			// find minimum in right tree

			var rightTreeRoot = nodeFindResult.node.RightDescendant;
			var leftmostNodeParrent = rightTreeRoot;
			var leftmostNode = rightTreeRoot;
			while (rightTreeRoot != null)
			{
				leftmostNode = rightTreeRoot;
				leftmostNodeParrent = rightTreeRoot;
				rightTreeRoot = rightTreeRoot.LeftDescendant;

			}

			leftmostNodeParrent.LeftDescendant = null;

			if (nodeFindResult.isNodeRightDescendant)
			{
				nodeFindResult.parentNode.RightDescendant = leftmostNode;
			}
			else
			{
				nodeFindResult.parentNode.LeftDescendant = leftmostNode;
			}
		}
		
		public string Print()
		{
			throw new NotImplementedException();
		}

		private BTreeNode<TKey, TValue> FindNode(TKey key)
		{
			var root = Root;
			while (root != null)
			{
				var comparisonResult = key.CompareTo(root.Key);

				if (comparisonResult == 0)
				{
					// means equal keys
					return root;
				}

				if (comparisonResult < 0)
				{
					// nodeKey < root Key
					root = root.LeftDescendant;
				}

				if (comparisonResult > 0)
				{
					// nodeKey > root Key

					root = root.RightDescendant;
				}
			}

			return null;
		}

		private (BTreeNode<TKey, TValue> node, BTreeNode<TKey, TValue> parentNode, bool isNodeRightDescendant) FindNodeWithParent(TKey key)
		{
			var node = Root;
			BTreeNode<TKey, TValue> parentNode = null;
			bool isNodeRightDescendant = false;

			while (node != null)
			{
				var comparisonResult = key.CompareTo(node.Key);

				if (comparisonResult == 0)
				{
					// means equal keys
					return (node, parentNode, isNodeRightDescendant);
				}

				parentNode = node;

				if (comparisonResult < 0)
				{
					// nodeKey < Key
					node = node.LeftDescendant;
					isNodeRightDescendant = false;
				}

				if (comparisonResult > 0)
				{
					// nodeKey > Key
					node = node.RightDescendant;
					isNodeRightDescendant = true;
				}
			}

			return (null, parentNode, isNodeRightDescendant);
		}
	}
}
