using System.Collections;
using System.Collections.Generic;using System.Data;
using UnityEngine;

public abstract class BaseFactoryCollection<T> : ScriptableObject
{
    protected HashSet<T> _data = new HashSet<T>();
    
    public IEnumerable<T> Data
    {
        get
        {
            foreach (var value in _data)
                yield return value;
        }
    }
}

public class CollectionEntities<T> : ICollectionEntities<T>
{
    private HashSet<T> _data = new HashSet<T>();

    public int CountSpawners => _data.Count;
    
    public IEnumerable<T> Data
    {
        get
        {
            foreach (var value in _data)
                yield return value;
        }
    }

    void ICollectionEntities<T>.Remove(T entity) => _data.Remove(entity);
    void ICollectionEntities<T>.Add(T entity) => _data.Add(entity);
}

public interface ICollectionEntities<T>
{
    void Remove(T entity);
    void Add(T entity);
}
