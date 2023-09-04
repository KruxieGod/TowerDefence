using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class GameEnemyFactory : ScriptableObject
{
    [SerializeField] private Enemy _prefabElf;
    [SerializeField] private Enemy _prefabHuman;
    [SerializeField] private Enemy _prefabOrc;
    private const string PATHTOFILES = "C:\\Users\\user\\Documents\\GitHub\\TowerDefence\\Tower Defense\\Assets\\Json Files\\";
    public Enemy GetPrefabEnemy(EnemyInfo enemyInfo)
    {
        switch (enemyInfo.EnemyType)
        {
            case (EnemyType.Human):
                return Instantiate(_prefabHuman).InitializeEnemy(enemyInfo.Behaviour);
            case (EnemyType.Elf):
                return Instantiate(_prefabElf).InitializeEnemy(enemyInfo.Behaviour);
            case (EnemyType.Orc):
                return Instantiate(_prefabOrc).InitializeEnemy(enemyInfo.Behaviour);
        }

        throw new ArgumentException("Wrong type:" + enemyInfo.EnemyType);
    }
}
