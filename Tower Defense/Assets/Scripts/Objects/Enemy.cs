using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BehaviourEnemy _behaviour;
    private Tile _currentTile;
    public const float RADIUS = GameBoard.POSITIONMULTIPLIER/2;
    private GameEnemyFactory _factory;
    private Direction _previousDirection;
    private float _timeToRotate = 0f;
    public void Initialize(BehaviourEnemy behaviour,Tile currentTile,GameEnemyFactory factory)
    {
        _previousDirection = currentTile.Direction;
        _factory = factory;
        transform.position = currentTile.transform.position;
        _behaviour = behaviour;
        _currentTile = currentTile;
        transform.rotation = currentTile.Direction.GetDirection();
    }

    public void UpdatePos()
    {
        if (_currentTile.NextTile == null)
        {
            Destroy(gameObject);
            return;
        }

        var distance = Vector3.Distance(transform.position, _currentTile.NextTile.transform.position);
        
        if (distance <= RADIUS)
        {
            _currentTile = _currentTile.NextTile;
            _previousDirection = _currentTile.Direction;
        }
        
        float speedRotation = _factory.SpeedRotation/2f;
        if (distance <= RADIUS / 2f)
            speedRotation *= 8f;
        transform.rotation = Quaternion.Lerp(transform.rotation,_currentTile.Direction.GetDirection(),speedRotation*Time.deltaTime*_behaviour.Speed);
        
        if (_previousDirection != _currentTile.Direction)
            _timeToRotate = 0.5f;
        
        if (_timeToRotate <= 0)
            transform.position += transform.forward* _behaviour.Speed*Time.deltaTime;
        else
            _timeToRotate -= Time.deltaTime;
        _previousDirection = _currentTile.Direction;
    }
}
