using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    public class Algorithms<T>
    {
        static public (bool,T) Find(ICollection<T> collection, Func<T, bool> f) 
        {
            collection.ResetIterators();
            IIterator<T> it = collection.forwardIterator;
            
            if(f(it.Value) == true)
            {
                return (true,it.Value);
            }

            while (it.HasNext() == true)
            {
                it = collection.forwardIterator;
                if (f(it.Value) == true)
                {
                    return (true, it.Value);
                }
            }
            return (false, default(T));
        }
        static public void ForEach(ICollection<T> collection, Action<T> f)
        {
            collection.ResetIterators();
            IIterator<T> it = collection.forwardIterator;
            f(it.Value);

            while (it.HasNext() == true)
            {
                it = collection.forwardIterator;
                f(it.Value);
            }
        }
        static public int CountIf(ICollection<T> collection, Func<T, bool> f)
        {
            collection.ResetIterators();
            IIterator<T> it = collection.forwardIterator;
            int sum = 0;

            if (f(it.Value) == true)
            {
                sum++;
            }

            while (it.HasNext() == true)
            {
                it = collection.forwardIterator;
                if (f(it.Value) == true)
                {
                    sum++;
                }
            }
            return sum;
        }
    }
}
