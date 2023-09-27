using System;
using System.Collections;
using UnityEngine;

public class SelectingTiles : ISettable<TypeOfTile>
{
    private readonly GameTileFactory _gameTileFactory;
    private readonly GameTowerFactory _gameTowerFactory;
    private Lazy<GameManager> _gameManager;
    public SelectingTiles(GameTileFactory gameTileFactory,
        GameTowerFactory gameTowerFactory,
        Lazy<GameManager> gameManager)
    {
        _gameManager = gameManager;
        _gameTileFactory = gameTileFactory;
        _gameTowerFactory = gameTowerFactory;
    }
    
    public void Set(TypeOfTile type)
    {
        switch (type)
        {
            case TypeOfTile.Wall:
            case TypeOfTile.Destination:
                _gameManager.Value.StartCoroutine(
                    WaitingForUnselect(_gameTileFactory.GetContent(type),
                        content => _gameManager.Value.SetTileOnPath(type,content)));
                break;
            case TypeOfTile.Mortar:
                _gameManager.Value.StartCoroutine(
                    WaitingForUnselect(_gameTowerFactory.GetBallista(),
                        content => _gameManager.Value.SetTileOnPath(type, content)));
                break;
            case TypeOfTile.Laser:
                _gameManager.Value.StartCoroutine(
                    WaitingForUnselect(_gameTowerFactory.GetLaserTurret(),
                        content => _gameManager.Value.SetTileOnPath(type,content)));
                break;
        }
    }

    private IEnumerator WaitingForUnselect(TileContent content,Action<TileContent> action)
    {
        Debug.Log("Waiting for set tile");
        content.enabled = false;
        Physics.Raycast(GameManager._ray, out var hit,100,GameManager.Instance.FloorLayer);
        content.transform.position =  hit.point;
        while (!Input.GetMouseButtonUp(0))
        {
            Physics.Raycast(GameManager._ray, out var hits,100,GameManager.Instance.FloorLayer);
            content.transform.position = hits.point;
            yield return null;
        }
        action(content);
    }
}