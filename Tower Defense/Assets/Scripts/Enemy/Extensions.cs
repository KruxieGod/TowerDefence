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
            return tile.Content.IsEnded;
        return true;
    }
}

public static class EnemiesExtension
{
    public static IEnumerable<Enemy> GetEnemies(this Collider[] array)
    {
        return array.Select(x => x.transform.root.GetComponent<Enemy>());
    }

    public static void Destroy(this GameObject gameObject) => UnityEngine.Object.Destroy(gameObject);
}

public enum EnemyType
{
    Human,
    Elf,
    Orc
}
