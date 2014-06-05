using System;
using System.Collections.Generic;

namespace Navidad.Library
{
    public class PriorityQueue
    {
        List<int> P;
        int SZ;
        public PriorityQueue()
        {
            P = new List<int>();
            P.Add(-1); // inserts a dummy node in position 0
            SZ = 0;
        }

        private void swap(int indx1, int indx2)
        {

            int holder = P[indx1];
            P[indx1] = P[indx2];
            P[indx2] = holder;

        }

        public void Push(int value)
        {
            //put value in PQ
            P.Add(value);
            SZ++;

            int indx = SZ;
            while (indx > 1)
            {
                if (P[indx / 2] > P[indx])
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

                    if (P[temp] < P[temp1] && P[indx] > P[temp])
                    {
                        swap(temp, indx);
                        indx = temp;
                    }

                    else if (P[temp] > P[temp1] && P[indx] > P[temp1])
                    {
                        swap(temp1, indx);
                        indx = temp1;
                    }
                }
            }
        }
        public int Top()
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
        public int[] ToArray()
        {
            int[] arr = new int[SZ + 1];
            //gets the array managed inside the Priority Queue
            if (SZ == 0)
            {
                throw new InvalidOperationException("Priority Queue is empty");
            }
            else
            {

                for (int i = 0; i < SZ + 1; ++i)
                {
                    arr[i] = P[i];
                }
            }
            return arr;

        }
        public int[] ToSortedArray()
        {
            //gets the array manged inside the Priority Queue but sorted
            if (SZ == 0)
            {
                throw new InvalidOperationException("Priority Queue is empty");
            }
            else
            {
                P.Sort();
                return P.ToArray();

            }
        }


    }
}