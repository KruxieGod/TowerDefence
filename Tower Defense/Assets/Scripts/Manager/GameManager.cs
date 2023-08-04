using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameTileFactory _factory;
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Camera _camera;
    private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);
    void Start()
    {
        _gameBoard.Initialize(_size,_factory);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SetDestination();
    }

    private void SetDestination()
    {
        ISetterTile tile = _gameBoard.GetTile(_ray);
        tile.SetTypeTile(_factory.GetContent(TypeOfTile.Destination));
        _gameBoard.SetCorrectDirectionTo(tile);
    }
}
