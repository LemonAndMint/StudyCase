using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace Pool{

    //#TODO make a random access pool

    /*public class RandomAccessPool<T> : ObjectPool<T> where T : class
    {
        internal readonly Dictionary<T, bool> m_List;

        private readonly Func<T> m_CreateFunc;

        private readonly Action<T> m_ActionOnGet;

        private readonly Action<T> m_ActionOnRelease;

        private readonly Action<T> m_ActionOnDestroy;

        private readonly int m_MaxSize;

        internal bool m_CollectionCheck;

        public new int CountAll { get; private set; }

        public new int CountActive => CountAll - CountInactive;

        public new int CountInactive => m_List.Count;

        public RandomAccessPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000) : 
        base(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
        {
            if (createFunc == null)
            {
                throw new ArgumentNullException("createFunc");
            }

            if (maxSize <= 0)
            {
                throw new ArgumentException("Max Size must be greater than 0", "maxSize");
            }

            m_List = new Dictionary<T, bool>(defaultCapacity);
            m_CreateFunc = createFunc;
            m_MaxSize = maxSize;
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
            m_ActionOnDestroy = actionOnDestroy;
            m_CollectionCheck = collectionCheck;
        }

        public new T Get()
        {
            T val;
            if (m_List.Count == 0)
            {
                val = m_CreateFunc();
                CountAll++;
            }
            else
            {
                int index = m_List.Count - 1;
                val = m_List[index];
                m_List.RemoveAt(index);
            }

            m_ActionOnGet?.Invoke(val);
            return val;
        }

        public new PooledObject<T> Get(out T v)
        {
            return new PooledObject<T>(v = Get(), this);
        }

        public new void Release(T element)
        {
            if (m_CollectionCheck && m_List.Count > 0)
            {
                for (int i = 0; i < m_List.Count; i++)
                {
                    if (element == m_List[i])
                    {
                        throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
                    }
                }
            }

            m_ActionOnRelease?.Invoke(element);
            if (CountInactive < m_MaxSize)
            {
                m_List[element] = true;
                return;
            }

            CountAll--;
            m_ActionOnDestroy?.Invoke(element);
        }

        public new void Clear()
        {
            if (m_ActionOnDestroy != null)
            {
                foreach (T item in m_List)
                {
                    m_ActionOnDestroy(item);
                }
            }

            m_List.Clear();
            CountAll = 0;
        }

        public new void Dispose()
        {
            Clear();
        }
    }*/

}
