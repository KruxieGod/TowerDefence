using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour,IDamagable
{
    private BehaviourEnemy _behaviour;
    private Tile _currentTile;
    public const float RADIUS = GameBoard.POSITIONMULTIPLIER/2;
    private GameEnemyFactory _factory;
    private Direction _previousDirection;
    private const float _distance = 1f;
    private Action<Enemy> _onDestroy;
    public int HealthPoints { get; private set; }

    public void Initialize(BehaviourEnemy behaviour,Tile currentTile,GameEnemyFactory factory,Action<Enemy> onDestroy)
    {
        _onDestroy = onDestroy;
        _previousDirection = currentTile.Direction;
        _factory = factory;
        transform.position = currentTile.transform.position;
        _behaviour = behaviour;
        _currentTile = currentTile;
        transform.rotation = currentTile.Direction.GetDirection();
        HealthPoints = behaviour.HP;
    }

    public void UpdatePos()
    {
        Debug.Log("Start");
        if (_currentTile.NextTile == null)
        {
            Destroy(gameObject);
            return;
        }

        if (Physics.Raycast (new Vector3(transform.position.x,transform.position.y+1f,transform.position.z),
                Vector3.down, out var hit,_distance,_factory.LayerFloor)
            && _currentTile.transform != hit.transform
            && hit.transform.TryGetComponent(out Tile tile))
        {
            _currentTile = tile;
            _previousDirection = _currentTile.Direction;
        }
        float speedRotation = _factory.SpeedRotation/2f;

        if (_previousDirection != _currentTile.Direction)
            speedRotation = 100f;

        transform.position += transform.forward* _behaviour.Speed*Time.deltaTime;
        
        transform.rotation = Quaternion.Lerp(transform.rotation,_currentTile.Direction.GetDirection(),speedRotation*Time.deltaTime*_behaviour.Speed);
        _previousDirection = _currentTile.Direction;
    }

    private void OnDestroy()
    {
        _onDestroy(this);
    }

    void IDamagable.TakeDamage(int damage)
    {
        if ((HealthPoints -= damage) <= 0)
            Destroy(gameObject);
    }
}
