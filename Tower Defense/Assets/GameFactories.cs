using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactories : MonoBehaviour
{
    [field : SerializeField] public GameTileFactory GameTileFactory { get; private set; }
    [field : SerializeField] public GameTowerFactory GameTowerFactory { get; private set; }
    [field : SerializeField] private List<GameEnemyFactory> _enemyFactories;

    public GameEnemyFactory GetEnemyFactoryFrom(string str)
    {
        foreach (var factory in _enemyFactories)
            if (factory.name == str)
                return factory;
        throw new ArgumentException("Factory is not founded");
    }
}
