using System.Collections;
using System.Collections.Generic;
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
