using System;
using System.Collections.Generic;

namespace Navidad.Library
{
    public class AVLTree
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
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            //Call this when Rotating (really important)
            public void UpdateFromChild()
            {
                Deepness = 0;
                if (Left != null) Deepness = Math.Max(Left.Deepness + 1, Deepness);
                if (Right != null) Deepness = Math.Max(Right.Deepness + 1, Deepness);
            }
            public int LeftDeepness()
            {
                return Left == null ? 0 : Left.Deepness;
            }
            public int RightDeepness()
            {
                return Right == null ? 0 : Right.Deepness;
            }
        }

        Node root;

        int Size { get; set; }
        public AVLTree()
        {
            Size = 0;
            root = null;
        }

        public int BalanceFactor(int value)
        {

            Node arr = new Node(value);
            if (root != null)
            {
                Node temp = root;
                while (true)
                {
                    if (temp.Value < arr.Value && temp.Right != null)
                        temp = temp.Right;

                    if (temp.Value > arr.Value && temp.Left != null)
                        temp = temp.Left;

                    if (temp.Value == arr.Value)
                        break;
                }
                arr = temp;


            }
            return BalanceFactor(arr);

        }
        private static int BalanceFactor(Node node)
        {
            int left = 0;
            int right = 0;
            if (node != null)
            {
                if (node.Left == null)
                    left = -1;
                else
                    left = node.LeftDeepness();
                if (node.Right == null)
                    right = -1;
                else
                    right = node.RightDeepness();

            }
            return left - right;


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
                Changeroot(newNode);
            }
        }

        private void Changeroot(Node node)
        {
            while (node.Parent != null)
            {
                root = node.Parent;
                node = node.Parent;
            }

        }


        /// <summary>
        /// Insert the specified p and value.
        /// </summary>
        /// <param name='p'>node that should be the parent</param>
        /// <param name='value'>value</param>
        private static void Insert(Node p, Node value)
        {
            Stack<Node> camp = new Stack<Node>();
            while (true)
            {
                if (value.Value > p.Value)
                {
                    value.Parent = p;
                    camp.Push(p);
                    if (p.Right == null)
                        break;
                    p = p.Right;
                }

                if (value.Value < p.Value)
                {
                    value.Parent = p;
                    camp.Push(p);
                    if (p.Left == null)
                        break;
                    p = p.Left;
                }
            }

            if (value.Value > p.Value)
                p.Right = value;
            else
                p.Left = value;

            foreach (var item in camp)
            {
                item.UpdateFromChild();
                Balance(item);
            }

            camp.Clear();

        }

        private static void setDeepness(Node current)
        {
            if (current == null) return;
            setDeepness(current.Left);
            setDeepness(current.Right);
            current.UpdateFromChild();
        }



        private static void Balance(Node node)
        {
            //check Deepness of both childs and balance accordin
            //Stack<Node> holder = new Stack<Node>();
            int balance = BalanceFactor(node);
            while (true)
            {
                if (balance <= 1 && balance >= -1)
                    break;

                if (balance > 1)
                {
                    if (BalanceFactor(node.Left) >= 1)
                    {
                        RotateRight(node.Left, node);
                    }
                    else
                    {
                        RotateLeft(node.Left.Right, node.Left);
                        RotateRight(node.Left, node);
                    }
                    break;
                }
                else if (balance < -1)
                {
                    if (BalanceFactor(node.Right) <= -1)
                    {
                        RotateLeft(node.Right, node);
                    }
                    else
                    {
                        RotateRight(node.Right.Left, node.Right);
                        RotateLeft(node.Right, node);
                    }
                    break;
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
            child.UpdateFromChild();
            parent.UpdateFromChild();
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
            node.UpdateFromChild();
            parent.UpdateFromChild();
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

        private void BalanceTillRoot(Node node)
        {
            Node temp = node;
            Node temp1 = root;
            Stack<Node> holder = new Stack<Node>();
            while (true)
            {
                holder.Push(temp1);
                if (temp1.Value < temp.Value)
                    temp1 = temp1.Right;
                else if (temp1.Value > temp.Value)
                    temp1 = temp1.Left;
                else if (temp1.Value == temp.Value)
                    break;
            }
            foreach (var item in holder)
            {
                item.UpdateFromChild();
                Balance(item);
            }
        }

        public void Remove(int value)
        {
            if (!Exist(value)) return;
            //else
            //use LowerBound(value+1) to get the number you need
            else
            {
                Node toDelete;
                toDelete = _LowerBound(value);
                Node temp = toDelete.Parent;
                Node temp1, temp2;
                if (toDelete.Left == null && toDelete.Right == null)
                {
                    if (!toDelete.isRoot())
                    {
                        if (temp.Value > value)
                            temp.Left = null;
                        else
                            temp.Right = null;

                        BalanceTillRoot(temp);
                    }
                    else
                        root = null;
                }
                else if (toDelete.Left == null && toDelete.Right != null)
                {
                    temp1 = toDelete.Right;
                    if (!toDelete.isRoot())
                    {
                        if (temp.Value > value)
                            temp.Left = temp1;
                        else
                            temp.Right = temp1;

                        temp1.Parent = temp;
                        temp1.UpdateFromChild();
                        BalanceTillRoot(temp1);
                    }
                    else
                        root = toDelete.Right;
                    BalanceTillRoot(root);
                }
                else if (toDelete.Left != null)
                {

                    temp2 = toDelete.Left;
                    temp1 = temp2;
                    if (temp2.Right == null)
                    {
                        if (!temp2.isRoot())
                        {
                            temp1.Right = toDelete.Right;
                            toDelete.Right.Parent = temp1;
                            if (temp.Value > value)
                                temp.Left = temp1;
                            else
                                temp.Right = temp1;

                            temp1.Parent = temp;
                            BalanceTillRoot(temp1);
                        }
                        else
                        {
                            temp2.Right = toDelete.Right;
                            root = temp2;
                            BalanceTillRoot(temp2);
                        }
                    }
                    else
                    {
                        while (temp2.Right != null)
                        {
                            temp2 = temp2.Right;
                        }
                        if (temp2.Left != null)
                            temp2.Parent.Right = temp2.Left;
                        else if (temp2.Right != null)
                            temp2.Parent.Right = temp2.Right;
                        else if (temp2.Right == null && temp2.Left == null)
                            temp2.Parent.Right = null;

                        Node head = temp2.Parent;
                        temp2.Right = toDelete.Right;
                        temp2.Left = toDelete.Left;
                        toDelete.Left.Parent = temp2;
                        toDelete.Right.Parent = temp2;
                        if (!toDelete.isRoot())
                        {
                            temp2.Parent = toDelete.Parent;

                            if (toDelete.Parent.Value > value)
                                toDelete.Parent.Left = temp2;
                            else if (toDelete.Parent.Value < value)
                                toDelete.Parent.Right = temp2;
                        }
                        else
                        {
                            temp2.Parent = null;
                            Changeroot(head);
                        }

                        BalanceTillRoot(head);
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