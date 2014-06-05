using System;
using System.Collections.Generic;

namespace Navidad.Library
{
    public class SplayTree
    {
        private class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }
            public int Value { get; set; }
            public int Deepness { get; set; }
            public Node()
            {
                Left = null;
                Right = null;
                Parent = null;
                Deepness = 0; // when node is alone Deepness is 0
            }
            public Node(int Value)
                : this() // builds a node using function above
            {
                this.Value = Value;
            }
            public bool isRoot()
            {
                // determines if node is a root (if has no parent)
                if (Parent == null)
                    return true;

                else
                    return false;
            }
        }

        Node root;

        int Size { get; set; }
        public SplayTree()
        {
            Size = 0;
            root = null;
        }

        /// <summary>
        /// Calls Insert(root,value) which can do recursion
        /// </summary>
        /// <param name='value'>integer to be inserted</param>
        public void Insert(int value)
        {
            if (this.Exist(value)) return;
            Node newNode = new Node(value);
            if (root == null)
            {
                root = newNode;
            }
            else
            {
                Insert(root, newNode);
                if (!newNode.isRoot())
                {
                    Node temp = newNode.Parent;
                    while (newNode != root)
                    {
                        Splay(newNode);
                        Changeroot(temp);
                    }
                }
            }
        }

        /// <summary>
        /// Insert the specified p and value.
        /// </summary>
        /// <param name='p'>node that should be the parent</param>
        /// <param name='value'>value</param>
        private static void Insert(Node p, Node value)
        {
            while (true)
            {
                if (value.Value > p.Value)
                {
                    value.Parent = p;
                    if (p.Right == null)
                        break;
                    p = p.Right;
                }

                if (value.Value < p.Value)
                {
                    value.Parent = p;
                    if (p.Left == null)
                        break;
                    p = p.Left;
                }
            }

            if (value.Value > p.Value)
                p.Right = value;
            else
                p.Left = value;
        }

        private void Changeroot(Node node)
        {
            while (node.Parent != null)
            {
                root = node.Parent;
                node = node.Parent;
            }

        }

        private static void Splay(Node node)
        {
            //promote "node" until is Root
            if (node.Parent.Value > node.Value)
            {
                Node temp = node.Parent;
                if (temp.Parent != null)
                {
                    if (temp.Parent.Value > temp.Value && temp != null)
                    {
                        RotateRight(temp, temp.Parent);
                        RotateRight(node, temp);
                    }
                    else if (temp.Parent.Value < temp.Value && temp != null)
                    {
                        Node temp1 = temp.Parent;
                        RotateRight(node, temp);
                        RotateLeft(node, temp1);
                    }
                }
                else
                {
                    RotateRight(node, node.Parent);
                }
            }
            else if (node.Parent.Value < node.Value)
            {
                Node temp = node.Parent;
                if (temp.Parent != null)
                {
                    if (temp.Parent.Value < temp.Value)
                    {
                        RotateLeft(temp, temp.Parent);
                        RotateLeft(node, temp);
                    }
                    else if (temp.Parent.Value > temp.Value)
                    {
                        Node temp1 = temp.Parent;
                        RotateLeft(node, temp);
                        RotateRight(node, temp1);
                    }
                }
                else
                {
                    RotateLeft(node, node.Parent);
                }
            }

        }


        /// <summary>
        /// rotates a subtree to the LEFT.
        /// makes "child" as the parent of "parent" (promotes "child")
        /// NOTE: 
        ///     this is a static function since you pass the nodes
        ///     and this does not depends in a property of an
        ///     instance
        /// 
        ///         BEFORE             AFTER
        /// -----------------    ------------------    
        ///        parent                  child  
        ///       /     \              /     \  
        ///      *     child        parent    * 
        ///           /    \       /     \       
        ///         *       *     *       *      
        /// 
        /// </summary>
        /// <param name='child'>is the right child of "parent"</param>
        /// <param name='parent'>is the parent of "child</param>
        private static void RotateLeft(Node child, Node parent)
        {
            if (child != null && parent != null)
            {
                Node temp = child.Left;
                Node temp1 = parent.Parent;

                if (!parent.isRoot())
                {
                    if (temp1.Value > parent.Value)
                        temp1.Left = child;
                    else
                        temp1.Right = child;
                }

                child.Left = parent;
                child.Parent = temp1;
                parent.Right = temp;
                parent.Parent = child;
                if (temp != null)
                    temp.Parent = parent;
            }
        }



        /// <summary>
        /// rotates a subtree to the RIGHT.
        /// makes "child" as the parent of "parent" (promotes "child")
        /// NOTE: 
        ///     this is a static function since you pass the nodes
        ///     and this does not depends in a property of an
        ///     instance
        ///     
        ///      BEFORE                AFTER
        /// -----------------    ------------------    
        ///        parent              child
        ///       /     \          /     \
        ///    child     *        *      parent
        ///   /    \                   /      \
        /// *       *                *         *
        /// 
        /// </summary>
        /// <param name='child'>is the left child of "parent"</param>
        /// <param name='parent'>is the parent of "child</param>
        private static void RotateRight(Node node, Node parent)
        {
            if (node != null && parent != null)
            {
                Node temp = node.Right;
                Node temp1 = parent.Parent;
                if (!parent.isRoot())
                {
                    if (temp1.Value > parent.Value)
                        temp1.Left = node;
                    else
                        temp1.Right = node;
                }

                node.Right = parent;
                node.Parent = temp1;
                parent.Left = temp;
                parent.Parent = node;
                if (temp != null)
                    temp.Parent = parent;
            }
        }

        public bool Exist(int value)
        {
            //if LowerBound implemented correctly
            return this.LowerBound(value) == value;
            //else
            /*
             * Implement this your own way
             * throw new NotImplementedException();
             */
        }

        public int LowerBound(int value)
        {
            return this._LowerBound(value).Value;
        }

        /// <summary>
        /// Return the first existing number which is >= value
        /// </summary>
        /// <returns>closest number</returns>
        /// <param name='value'>Value</param>
        private Node _LowerBound(int value)
        {
            Node temp = root;
            Node holder = temp;
            if (root == null)
                return new Node(int.MinValue);
            else
            {
                // traverse the tree to look for this value
                while (true)
                {
                    if (temp.Value == value)
                    {
                        return temp;
                    }
                    if (temp.Value > value)
                    {
                        holder = temp;
                        if (temp.Left == null)
                            break;

                        temp = temp.Left;
                    }
                    else if (temp.Value < value)
                    {
                        if (temp.Right == null)
                        {
                            if (holder.Value >= value)
                                break;
                            else
                                return new Node(int.MaxValue);
                        }
                        temp = temp.Right;
                    }
                }
                return holder;
            }
        }

        public void Remove(int value)
        {
            if (!Exist(value)) return;
            //else
            //use LowerBound(value+1) to get the number you need
            Node toDelete = _LowerBound(value);
            if (toDelete.isRoot())
            {
                if (toDelete.Left == null && toDelete.Right == null)
                    root = null;
            }
            else
            {
                Node temp = null;
                if (!toDelete.isRoot())
                    temp = toDelete.Parent;

                while (toDelete.Parent != null)
                {
                    Splay(toDelete);
                    Changeroot(temp);
                }
                if (toDelete.Left == null && toDelete.Right != null)
                {
                    root = toDelete.Right;
                    toDelete.Right.Parent = null;
                }
                else if (toDelete.Left != null)
                {
                    Node temp1;
                    if (toDelete.Left.Right == null)
                    {
                        temp1 = toDelete.Right;
                        root = toDelete.Left;
                        toDelete.Left.Parent = null;
                        root.Right = temp1;
                    }
                    else
                    {
                        temp1 = toDelete.Left.Right;
                        while (temp1.Right != null)
                        {
                            temp1 = temp1.Right;
                        }
                        if (temp1.Left != null)
                            temp1.Parent = temp1.Left;
                        else if (temp1.Right != null)
                            temp1.Parent = temp1.Right;

                        root.Value = temp1.Value;

                    }
                }
            }
        }

        public int[] ToArray()
        {
            List<int> L = new List<int>();
            ToArray(root, L);
            return L.ToArray();
        }

        private static void ToArray(Node node, List<int> L)
        {
            if (node == null) return;
            ToArray(node.Left, L);
            L.Add(node.Value);
            ToArray(node.Right, L);
        }
    }
}