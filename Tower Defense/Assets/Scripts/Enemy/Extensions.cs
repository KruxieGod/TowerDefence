using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TileExtension
{
    public static bool HasPath(this Tile tile)
    {
        while (tile.NextTile != null)
            tile = tile.NextTile;
        return tile.Content.TileType == TypeOfTile.Destination;
    }

    public static bool CanBeSet(this Tile tile,GameBoard gameBoard,GameEnemyFactory enemyFactory)
    {
        if (tile.Content.TileType == TypeOfTile.Destination && gameBoard.CountDestinations == 1)
            return false;
        else if (tile.Content.TileType == TypeOfTile.SpawnerEnemy)
            return enemyFactory.CanRemoveSpawner(tile);
        return true;
    }
}

public static class EnemiesExtension
{
    private static Dictionary<EnemyType, BehaviourEnemy> _behaviours =
        new ()
        {
            { EnemyType.Human, new BehaviourEnemy(9) },
            { EnemyType.Elf, new BehaviourEnemy(14) },
            { EnemyType.Orc, new BehaviourEnemy(5) }
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
