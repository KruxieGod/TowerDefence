using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = UnityEditor.VersionControl.Task;

public static class TileExtension
{
    public static bool HasPath(this Tile tile)
    {
        while (tile.NextTile != null)
            tile = tile.NextTile;
        return tile.Content.TileType == TypeOfTile.Destination;
    }

    public static bool CanBeSet(this Tile tile,GameBoard gameBoard)
    {
        if (tile.Content.TileType == TypeOfTile.Destination && gameBoard.CountDestinations == 1)
            return false;
        return tile.Content.TileType != TypeOfTile.SpawnerEnemy;
    }
}

public static class JsonExtension
{
    public static void SerializeClass<T>(T instance,string path)
        where T : class
    {
        var json = JsonUtility.ToJson(instance);
        System.IO.File.WriteAllText(path,json);
    }
    
    public static T GetClassFromJson<T>(string path)
    where T : class
    {
        try
        {
            string jsonText = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<T>(jsonText);
        }
        catch
        {
            return null;
        }
    }

    public static IEnumerable<T> GetEnumerableClassFromJson<T>(string path)
    where T : class
    {
        string[] jsonFiles = System.IO.Directory.GetFiles(path, "*.json");
        foreach (var jsonPath in jsonFiles)
            yield return GetClassFromJson<T>(jsonPath);
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

public static class AsyncExtension
{
    public static T WaitAndGetResult<T>(this Task<T> awaiter)
    {
        awaiter.Wait();
        return awaiter.Result;
    }
}

public enum EnemyType
{
    Human,
    Elf,
    Orc
}
