using System;
using System.Collections.Generic;

namespace Navidad.Library
{
    public class GPriorityQueue<T> where T : IComparable
    {
        List<T> P;
        int SZ;
        public GPriorityQueue()
        {
            P = new List<T>();
            P.Add(default(T)); // inserts a dummy node in position 0
            SZ = 0;
        }

        private void swap(int indx1, int indx2)
        {

            T holder = P[indx1];
            P[indx1] = P[indx2];
            P[indx2] = holder;

        }

        public void Push(T value)
        {
            //put value in PQ
            P.Add(value);
            SZ++;

            int indx = SZ;
            while (indx > 1)
            {
                if (P[indx / 2].CompareTo(P[indx]) > 0)
                    swap(indx / 2, indx);
                indx /= 2;
            }
        }

        public void Pop()
        {
            //pops the element at the top of the queue
            if (SZ == 0)
            {
                throw new InvalidOperationException("Priority Queue is empty");
            }
            else
            {
                P[1] = P[SZ];
                P.RemoveAt(SZ);
                SZ--;


                int indx = 1;
                while (indx < SZ)
                {
                    int temp = 2 * indx;
                    int temp1 = temp + 1;


                    if (temp > SZ)
                        break;

                    if (P[temp].CompareTo(P[temp1]) < 0 && P[indx].CompareTo(P[temp]) > 0)
                    {
                        swap(temp, indx);
                        indx = temp;
                    }

                    else if (P[temp].CompareTo(P[temp1]) > 0 && P[indx].CompareTo(P[temp1]) > 0)
                    {
                        swap(temp1, indx);
                        indx = temp1;
                    }
                }
            }
        }
        public T Top()
        {
            // gets the element at the top of the queue
            if (SZ == 0)
            {
                throw new InvalidOperationException("Priority Queue is empty");
            }
            else
            {
                return P[1];
            }
        }
        public int Size()
        {
            return SZ - 1;
        }
        public T[] ToArray()
        {
            //gets the array managed inside the Priority Queue
            T[] arr = new T[SZ];
            if (SZ == 0)
            {
                throw new InvalidOperationException("Priority is empty");
            }
            else
            {
                for (int i = 1; i < SZ; i++)
                {
                    arr[i] = P[i];
                }
            }
            return arr;
        }
        public T[] ToSortedArray()
        {
            //gets the array manged inside the Priority Queue but sorted
            if (SZ == 0)
            {
                throw new InvalidOperationException("Priority is empty");
            }
            else
            {
                P.Sort();
                return P.ToArray();
            }
        }

    }
}