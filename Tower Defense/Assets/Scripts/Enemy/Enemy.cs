using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour,IDamagable
{
    [SerializeField] private float _speedRotation;
    [SerializeField] private LayerMask _layerFloor;
    private BehaviourEnemy _behaviour;
    private Tile _currentTile;
    public const float RADIUS = GameBoard.POSITIONMULTIPLIER/2;
    private Direction _previousDirection;
    private const float _distance = 1f;
    public int HealthPoints { get; private set; }
    private Action<Enemy> _onDestroy;
    public float Speed => _behaviour.Speed;
    public void Initialize(
        Tile currentTile,
        Action<Enemy> onDestroy)
    {
        _onDestroy = onDestroy;
        _previousDirection = currentTile.Direction;
        transform.position = currentTile.transform.position;
        _currentTile = currentTile;
        transform.rotation = currentTile.Direction.GetDirection();
    }

    public Enemy InitializeEnemy(BehaviourEnemy behaviour)
    {
        _behaviour = behaviour;
        HealthPoints = behaviour.HP;
        return this;
    }

    public void UpdatePos()
    {
        Debug.Log("Start");
        if (_currentTile.NextTile == null)
        {
            PassedCounter.NotifyCounterOn?.Invoke(1);
            Destroy(gameObject);
            return;
        }

        if (Physics.Raycast (new Vector3(transform.position.x,transform.position.y+1f,transform.position.z),
                Vector3.down, out var hit,_distance,_layerFloor)
            && _currentTile.transform != hit.transform
            && hit.transform.TryGetComponent(out Tile tile))
        {
            _currentTile = tile;
            _previousDirection = _currentTile.Direction;
        }
        float speedRotation = _speedRotation/2f;

        if (_previousDirection != _currentTile.Direction)
            speedRotation = 100f;

        transform.position += transform.forward* _behaviour.Speed*Time.deltaTime;
        
        transform.rotation = Quaternion.Lerp(transform.rotation,_currentTile.Direction.GetDirection(),speedRotation*Time.deltaTime*_behaviour.Speed);
        _previousDirection = _currentTile.Direction;
    }

    void IDamagable.TakeDamage(int damage)
    {
        if ((HealthPoints -= damage) <= 0)
            GameManager.OnDestroy.AddListener(gameObject.Destroy);
    }

    private void OnDestroy()
    {
        _onDestroy(this);
    }
}
