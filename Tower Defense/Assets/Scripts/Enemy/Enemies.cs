using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemiesExtension
{
    private static Dictionary<EnemyType, BehaviourEnemy> _behaviours =
        new Dictionary<EnemyType, BehaviourEnemy>()
        {
            { EnemyType.Human, new BehaviourEnemy(2) },
            { EnemyType.Elf, new BehaviourEnemy(3) },
            { EnemyType.Orc, new BehaviourEnemy(1) }
        };

    public static BehaviourEnemy GetBehaviour(this EnemyType enemy)
    {
        return _behaviours[enemy];
    }
}

public enum EnemyType
{
    Human,
    Elf,
    Orc
}
