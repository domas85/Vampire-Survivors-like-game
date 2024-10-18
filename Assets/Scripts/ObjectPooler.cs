using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;


// dont worry where i got this
public class ObjectPooler<T> : IDisposable, IObjectPool<T> where T : class
{
    internal readonly List<T> m_List;

    private readonly Func<T, T> m_CreateFunc;

    private readonly Action<T> m_ActionOnGet;

    private readonly Action<T> m_ActionOnRelease;

    private readonly Action<T> m_ActionOnDestroy;


    private readonly int m_MaxSize;

    internal bool m_CollectionCheck;

    public int CountAll { get; private set; }

    public int CountActive => CountAll - CountInactive;

    public int CountInactive => m_List.Count;

    //public T targetValue;

    public ObjectPooler(Func<T, T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
    {
        if (createFunc == null)
        {
            Debug.Log("createFunc");
        }

        if (maxSize <= 0)
        {
            Debug.Log("Max Size must be greater than 0 maxSize");
        }

        m_List = new List<T>(defaultCapacity);
        m_CreateFunc = createFunc;
        m_MaxSize = maxSize;
        m_ActionOnGet = actionOnGet;
        m_ActionOnRelease = actionOnRelease;
        m_CollectionCheck = collectionCheck;
    }

    public T Get(T targetValueInPool)
    {
        T targetValue;

        if (m_List.Count == 0)
        {
            //val = m_CreateFunc();
            targetValue = m_CreateFunc(targetValueInPool);
            CountAll++;
        }
        else
        {
            int index = 0;
            bool isInPool = false;
            for (int i = 0; i < m_List.Count; i++)
            {        //Temp solution, becomes not generic
                if (((EnemyClass)(object)m_List[i]).enemyData.enemyType == ((EnemyClass)(object)targetValueInPool).enemyData.enemyType) //Can not figure out how to differentiate between the prefab and the clone of the prefab
                {
                    index = i;
                    targetValue = targetValueInPool;
                    isInPool = true;
                    break;
                }
            }
            if (isInPool)
            {
                targetValue = m_List[index];
                m_List.RemoveAt(index);
            }
            else
            {
                targetValue = m_CreateFunc(targetValueInPool);
                CountAll++;
            }
        }

        m_ActionOnGet?.Invoke(targetValue);
        return targetValue;

        //public T Get()
        //{
        //    T val;
        //    if (m_List.Count == 0)
        //    {
        //        val = m_CreateFunc();
        //        CountAll++;
        //    }
        //    else
        //    {
        //        int index = m_List.Count - 1;
        //        val = m_List[index];
        //        m_List.RemoveAt(index);
        //    }

        //    m_ActionOnGet?.Invoke(val);
        //    return val;
        //}
    }

    public PooledObject<T> Get(out T v, T Item)
    {
        return new PooledObject<T>(v = Get(Item), this);
    }

    public void Release(T element)
    {
        if (m_CollectionCheck && m_List.Count > 0)
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                if (element == m_List[i])
                {
                    throw new InvalidOperationException("Trying to release an object that has already been released to the pool."); // sometimes this happens and drops 2 xp coins
                }
            }
        }

        m_ActionOnRelease?.Invoke(element);
        if (CountInactive < m_MaxSize)
        {
            m_List.Add(element);
            return;
        }

        CountAll--;
        m_ActionOnDestroy?.Invoke(element);
    }

    public void Clear()
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

    public void Dispose()
    {
        Clear();
    }

}
public readonly struct PooledObject<T> : IDisposable where T : class
{
    private readonly T m_ToReturn;

    private readonly IObjectPool<T> m_Pool;

    public PooledObject(T value, IObjectPool<T> pool)
    {
        m_ToReturn = value;
        m_Pool = pool;
    }

    void IDisposable.Dispose()
    {
        m_Pool.Release(m_ToReturn);
    }
}
public interface IObjectPool<T> where T : class
{
    int CountInactive { get; }

    T Get(T index);

    PooledObject<T> Get(out T v, T Item);

    void Release(T element);

    void Clear();
}