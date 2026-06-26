using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    public interface ICollection<T>
    {
        public IIterator<T> forwardIterator { get; }
        public IIterator<T> reverseIterator { get; }
        public void ResetIterators();
        public void InsertFront(T val);
        public void InsertBack(T val);
        public void DeleteFront();
        public void DeleteBack();
    }

    public class DequeCollection<T> : ICollection<T>
    {
        public Deque<T> deque;
        public IIterator<T> forwardIt;
        public IIterator<T> reverseIt;
        bool firstVisitedFd = false;
        bool firstVisitedRv = false;

        public DequeCollection(Deque<T> _deque)
        {
            deque = _deque;
            reverseIt = new DequeReverseIterator<T>(_deque);
            forwardIt = new DequeForwardIterator<T>(_deque);
        }

        public IIterator<T> forwardIterator
        {
            get 
            {
                if(firstVisitedFd == false)
                {
                    firstVisitedFd = true;
                    return forwardIt;
                }

                if (forwardIt.HasNext())
                {
                    forwardIt.Next();
                }
                return forwardIt;
            }
        }

        public IIterator<T> reverseIterator
        {
            get
            {
                if (firstVisitedRv == false)
                {
                    firstVisitedRv = true;
                    return reverseIt;
                }

                if (reverseIt.HasNext())
                {
                    reverseIt.Next();
                }
                return reverseIt;
            }
        }

        public void DeleteBack()
        {
            deque.deleteBack();
            ResetIterators();
        }

        public void DeleteFront()
        {
            deque.deleteFront();
            ResetIterators();
        }

        public void InsertBack(T val)
        {
            deque.pushBack(val);
            ResetIterators();
        }

        public void InsertFront(T val)
        {
            deque.pushFront(val);
            ResetIterators();
        }

        public void ResetIterators()
        {
            reverseIt = new DequeReverseIterator<T>(deque);
            forwardIt = new DequeForwardIterator<T>(deque);
            firstVisitedFd = false;
            firstVisitedRv = false;
        }
    }

    public interface IIterator<T>
    {
        bool HasNext();
        void Next();
        T Value { get; set; }
    }

    public class DequeForwardIterator<T> : IIterator<T>
    {
        public Deque<T> deq;
        public T it;
        public int deqItem;
        public int deqLen;
        public int vecItem;
        public int vecLen;

        public DequeForwardIterator(Deque<T> _deq) 
        {
            deq = _deq;
            deqItem = 0;
            vecItem = _deq.itemFirst;
            deqLen = _deq.Length;
            vecLen = 10;
            it = deq.deque[0].vector[0];
        }

        public T Value 
        { 
            get
            {
                return it;
            }
            set
            {
                it = value;
            }
        }

        public bool HasNext()
        {
            if(deqItem == deqLen-1 && vecItem == deq.itemLast)
            {
                return false;
            }
            return true;
        }

        public void Next()
        {
            if(this.HasNext() == true)
            {
                if(vecItem == 9)
                {
                    deqItem++;
                    vecItem = 0;
                    it = deq.deque[deqItem].vector[vecItem];
                }
                else
                {
                    if(deqItem == 0)
                    {
                        vecItem++;
                        it = deq.deque[deqItem].vector[vecItem-deq.itemFirst];
                    }
                    else
                    {
                        vecItem++;
                        it = deq.deque[deqItem].vector[vecItem];
                    }
                }
            }
            else
            {
                it = default(T);
            }
        }
    }

    public class DequeReverseIterator<T> : IIterator<T>
    {
        public Deque<T> deq;
        public T it;
        public int deqItem;
        public int deqLen;
        public int vecItem;
        public int vecLen;

        public DequeReverseIterator(Deque<T> _deq)
        {
            deq = _deq;
            deqItem = _deq.Length-1;
            vecItem = _deq.itemLast;

            vecLen = 10;
            it = deq.deque[deqItem].vector[vecItem];
        }

        public T Value
        {
            get
            {
                return it;
            }
            set
            {
                it = value;
            }
        }

        public bool HasNext()
        {
            if (deqItem == 0 && vecItem == deq.itemFirst)
            {
                return false;
            }
            return true;
        }

        public void Next()
        {
            if (this.HasNext() == true)
            {
                if (vecItem == 0)
                {
                    deqItem--;
                    if (deqItem == 0)
                    {
                        vecItem = 9;
                        it = deq.deque[deqItem].vector[vecItem - deq.itemFirst];
                    }
                    else
                    {
                        vecItem = 9;
                        it = deq.deque[deqItem].vector[vecItem];
                    }
                }
                else
                {
                    if (deqItem == 0)
                    {
                        vecItem--;
                        it = deq.deque[deqItem].vector[vecItem - deq.itemFirst];
                    }
                    else
                    {
                        vecItem--;
                        it = deq.deque[deqItem].vector[vecItem];
                    }
                }
            }
            else
            {
                it = default(T);
            }
        }
    }
}
