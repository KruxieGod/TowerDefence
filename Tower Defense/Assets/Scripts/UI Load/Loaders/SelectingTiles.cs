using System;
using System.Collections;
using UnityEngine;

public class SelectingTiles : ISettable<TypeOfTile>
{
    private readonly GameTileFactory _gameTileFactory;
    private readonly GameTowerFactory _gameTowerFactory;
    private GameManager _gameManager => ProjectContexter.Instance.GameObjectsProvider.GameManager;
    public SelectingTiles(GameTileFactory gameTileFactory,
        GameTowerFactory gameTowerFactory)
    {
        _gameTileFactory = gameTileFactory;
        _gameTowerFactory = gameTowerFactory;
    }
    
    public void Set(TypeOfTile type)
    {
        switch (type)
        {
            case TypeOfTile.Wall:
            case TypeOfTile.Destination:
                _gameManager.StartCoroutine(
                    WaitingForUnselect(_gameTileFactory.GetContent(type),
                        content => _gameManager.SetTileOnPath(type,content)));
                break;
            case TypeOfTile.Mortar:
                _gameManager.StartCoroutine(
                    WaitingForUnselect(_gameTowerFactory.GetBallista(),
                        content => _gameManager.SetTileOnPath(type, content)));
                break;
            case TypeOfTile.Laser:
                _gameManager.StartCoroutine(
                    WaitingForUnselect(_gameTowerFactory.GetLaserTurret(),
                        content => _gameManager.SetTileOnPath(type,content)));
                break;
        }
    }

    private IEnumerator WaitingForUnselect(TileContent content,Action<TileContent> action)
    {
        Debug.Log("Waiting for set tile");
        content.enabled = false;
        content.transform.position = GameManager._ray.GetPoint(ProjectContexter.Instance.GameObjectsProvider.GameManager.DistanceFromCamera);
        while (!Input.GetMouseButtonUp(0))
        {
            content.transform.position = GameManager._ray.GetPoint(ProjectContexter.Instance.GameObjectsProvider.GameManager.DistanceFromCamera);
            yield return null;
        }
        action(content);
    }
}