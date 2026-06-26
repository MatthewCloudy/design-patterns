using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    public class Deque<T>
    {
        public int Length;
        public int itemFirst;
        public int itemLast;
        public List<Vector> deque;
        public class Vector
        {
            public List<T> vector;
            public Vector()
            {
                vector = new List<T>(10);
            }
        }
        public Deque()
        {
            deque = new List<Vector>();
            Length = 0;
            itemFirst = 0;
            itemLast = 0;
        }

        public void pushBack(T el)
        {
            if(Length == 0)
            {
                deque.Add(new Vector());
                deque[0].vector.Add(el);
                Length = 1;
                return;
            }
            if (itemLast == 9)
            {
                deque.Add(new Vector());
                itemLast = 0;
                Length++;
                deque[Length - 1].vector.Add(el);
            }
            else
            {
                deque[Length - 1].vector.Add(el);
                itemLast++;
            }
            return;
        }

        public void pushFront(T el)
        {
            if (Length == 0)
            {
                deque.Add(new Vector());
                deque[0].vector.Add(el);
                Length = 1;
                return;
            }
            if (itemFirst == 0)
            {
                deque.Insert(0, new Vector());
                itemFirst = 9;
                Length++;
                deque[0].vector.Add(el);
            }
            else
            {
                itemFirst--;
                deque[0].vector.Insert(0, el);
            }
            return;
        }

        public void deleteBack()
        {
            if (Length == 0)
            {
                return;
            }
            if (itemLast == 0)
            {
                deque.RemoveAt(Length - 1);
                itemLast = 9;
                Length--;
            }
            else
            {
                deque[Length - 1].vector.RemoveAt(deque[Length - 1].vector.Count-1);
                itemLast--;
            }
            return;
        }
        public void deleteFront()
        {
            if (Length == 0)
            {
                return;
            }
            if (itemFirst == 9)
            {
                deque.RemoveAt(0);
                itemFirst = 0;
                Length--;
            }
            else
            {
                deque[0].vector.RemoveAt(0);
                itemFirst++;
            }
            return;
        }
    }
}
