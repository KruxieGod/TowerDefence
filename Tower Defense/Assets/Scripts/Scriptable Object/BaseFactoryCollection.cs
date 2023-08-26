using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactoryCollection<T> : ScriptableObject
    where T : MonoBehaviour
{
    protected HashSet<T> _data = new HashSet<T>();
    
    public IEnumerable<T> Data
    {
        get
        {
            foreach (var spawner in _data)
                yield return spawner;
        }
    }

    protected T GetPrefab(T prefab)
    {
        if (_data.TryGetValue(prefab, out var spawner))
            return spawner;
        var enemySpawner = Initialization( Instantiate(prefab));
        _data.Add(enemySpawner);
        return enemySpawner;
    }

    protected abstract T Initialization(T prefab);
}
