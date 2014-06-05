using System;
using System.Collections.Generic;

namespace Navidad.Library
{
    public class Trie
    {
        private const int ALPHABET = 26;
        private class Node
        {
            public Node[] childs { get; set; }
            public bool StringEndsHere { get; set; }
            public Node()
            {
                this.childs = new Node[ALPHABET];
                this.StringEndsHere = false;
            }
            public static int getValue(char c)
            {
                if (!char.IsLetter(c))
                    throw new InvalidCastException();
                return char.ToLower(c) - 'a';
            }
        }

        private Node root;
        public int Size { get; private set; }
        public Trie()
        {
            root = new Node();// represents empty string
        }
        public void Clear()
        {
            root = new Node();
            Size = 0;
        }


        //NOTE: not recursion needed
        public void Insert(string s)
        {
            if (Exist(s)) return;
            // start at the root and traverse 
            // or create nodes when needed
            else
            {
                Node temp = root;
                for (int i = 0; i < s.Length; ++i)
                {
                    int indx = Node.getValue(s[i]);
                    if (temp.childs[indx] == null)
                        temp.childs[indx] = new Node();
                    if (i == s.Length - 1 && temp.childs[indx] != null)
                        temp.childs[indx].StringEndsHere = true;

                    temp = temp.childs[indx];
                }
                Size++;
            }
            //at the and set the "StringEndsHere" flag to true   
        }

        //NOTE: not recursion needed
        public bool Exist(string s)
        {
            //traverse from the root 
            //if one needed node is null
            //the string does not exist
            Node temp = root;

            for (int i = 0; i < s.Length; ++i)
            {
                int indx = Node.getValue(s[i]);
                if (temp.childs[indx] == null)
                    break;

                temp = temp.childs[indx];
                if (i == s.Length - 1)
                    return true;
            }
            //at the end if a String ends here return true
            return false;
        }

        //prints a the sorted list of strings in O(N)
        public string[] ToArray()
        {
            List<string> RET = new List<string>();
            ToArray("", root, RET);
            return RET.ToArray();
        }

        Stack<Node> holder;
        //recursion needed

        private void ToArray(string StringSoFar, Node node, List<string> L)
        {
            //puts the elements int the list  


            for (int i = 0; i < ALPHABET; ++i)
            {

                if (node.childs[i] != null)
                {
                    char attach = Convert.ToChar((i + 97));
                    StringSoFar += attach;
                    ToArray(StringSoFar, node.childs[i], L);
                    if (StringSoFar.Length == 1) StringSoFar = "";
                }

                if (node.StringEndsHere == true)
                {
                    if (!L.Contains(StringSoFar))
                        L.Add(StringSoFar);
                }

            }
        }


        //EXTRA: enlist all the strings that starts with "StringSoFar"
        public string[] Suggestions(string StringSoFar)
        {
            //find the last node prefix of StringSoFar
            Node lastNode = null;// <-- put this node here
            Node temp = root;
            for (int i = 0; i < StringSoFar.Length; ++i)
            {

                int indx = Node.getValue(StringSoFar[i]);
                if (temp.childs[indx] != null)
                    temp = temp.childs[indx];
            }
            lastNode = temp;
            List<string> RET = new List<string>();
            ToArray(StringSoFar, lastNode, RET);
            return RET.ToArray();
        }
    }
}