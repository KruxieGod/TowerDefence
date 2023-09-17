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

public interface IClamp
{
    FloatRange ToHuman(FloatRange floatRange);
    FloatRange ToElf(FloatRange floatRange);
    FloatRange ToOrc(FloatRange floatRange);
}

public class ClampSpeed : IClamp
{
    public FloatRange ToHuman(FloatRange floatRange) => new (Mathf.Clamp(floatRange.Min, 8f, 10f), Mathf.Clamp(floatRange.Max, 8f, 10f));
    public FloatRange ToElf(FloatRange floatRange) => new (Mathf.Clamp(floatRange.Min, 13f, 15f), Mathf.Clamp(floatRange.Max, 13f, 15f));
    public FloatRange ToOrc(FloatRange floatRange) =>new (Mathf.Clamp(floatRange.Min, 3f, 5f), Mathf.Clamp(floatRange.Max, 3f, 5f));
}

public class ClampHP : IClamp
{
    public FloatRange ToHuman(FloatRange floatRange) => new (Mathf.Clamp(floatRange.Min, 20f, 40f), Mathf.Clamp(floatRange.Max, 20f, 40f));
    public FloatRange ToElf(FloatRange floatRange) => new (Mathf.Clamp(floatRange.Min, 10f, 30f), Mathf.Clamp(floatRange.Max, 10f, 30f));
    public FloatRange ToOrc(FloatRange floatRange) =>new (Mathf.Clamp(floatRange.Min, 45f, 55f), Mathf.Clamp(floatRange.Max, 45f, 55f));
}

public class ClampPrice : IClamp
{
    public FloatRange ToHuman(FloatRange floatRange) => new (Mathf.Clamp(floatRange.Min, 8f, 12f), Mathf.Clamp(floatRange.Max, 8f, 12f));
    public FloatRange ToElf(FloatRange floatRange) => new (Mathf.Clamp(floatRange.Min, 6f, 10f), Mathf.Clamp(floatRange.Max, 6f, 10f));
    public FloatRange ToOrc(FloatRange floatRange) =>new (Mathf.Clamp(floatRange.Min, 14f, 20f), Mathf.Clamp(floatRange.Max, 14f, 20f));
}

public static class EnemiesExtension
{
    public static FloatRange ClampSpeed(this FloatRange floatRange, EnemyType enemyType) =>
        Clamp(floatRange, enemyType, new ClampSpeed());
    
    public static FloatRange ClampHP(this FloatRange floatRange, EnemyType enemyType) =>
        Clamp(floatRange, enemyType, new ClampHP());
    
    public static FloatRange ClampPrice(this FloatRange floatRange, EnemyType enemyType) =>
        Clamp(floatRange, enemyType, new ClampPrice());

    private static FloatRange Clamp(FloatRange floatRange, EnemyType enemyType, IClamp clamp)
    {
        if (enemyType == EnemyType.Human)
            return clamp.ToHuman(floatRange);
        if (enemyType == EnemyType.Elf)
            return clamp.ToElf(floatRange);
        return clamp.ToOrc(floatRange);
    }
    
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
