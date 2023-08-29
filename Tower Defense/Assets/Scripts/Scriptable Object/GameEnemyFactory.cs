using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEnemyFactory : BaseFactoryCollection<EnemySpawner>
{
    [SerializeField] private Enemy _prefabEnemy;
    public int CountSpawners => _data.Count;
}
