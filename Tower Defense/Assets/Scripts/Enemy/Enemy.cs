using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;

[SelectionBase,RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour,IDamagable,ICollector
{
    public bool IsTrigger { get; private set; }
    private EnemyView _enemyView;
    public const float SpeedRotation = 3f;
    private EnemyInfo _behaviour;
    private Tile _currentTile;
    public Vector3 CurrentTilePosition => _currentTile.transform.position;
    public const float RADIUS = GameBoard.POSITIONMULTIPLIER/2;
    public Direction PreviousDirection { get; private set; }
    public Direction CurrentDirection => _currentTile.Direction;
    public const float Distance = 1f;
    public int HealthPoints { get; private set; }
    private Action<Enemy> _onDestroy;
    public float Speed => _behaviour.Speed;
    private Action<int> _addMoney;
    public void Initialize(
        Tile currentTile,
        Action<int> addMoney,
        Action<Enemy> onDestroy)
    {
        _addMoney = addMoney;
        _enemyView = GetComponentInChildren<EnemyView>();
        _onDestroy = onDestroy;
        PreviousDirection = currentTile.Direction;
        transform.position = currentTile.transform.position;
        _currentTile = currentTile;
        transform.rotation = currentTile.Direction.GetDirection();
    }

    public Enemy InitializeEnemy(EnemyInfo behaviour)
    {
        _behaviour = behaviour;
        HealthPoints = behaviour.HP;
        return this;
    }
    
    public void UpdatePos()
    {
        transform.position = GetDirection();
        transform.rotation = GetRotation();
    }

    public Vector3 GetDirection()
    {
        if (_currentTile.Content.TileType == TypeOfTile.Destination)
        {
            PassedCounter.NotifyCounterOn?.Invoke(1);
            Die();
        }
        
        PreviousDirection = _currentTile.Direction;
        if (Physics.Raycast (new Vector3(transform.position.x,transform.position.y+1f,transform.position.z),
                Vector3.down, out var hit,Distance,ProjectContexter.Instance.LayerFloor)
            && _currentTile.transform != hit.transform
            && hit.transform.TryGetComponent(out Tile tile))
        {
            _currentTile = tile;
            PreviousDirection = _currentTile.Direction;
        }

        return transform.position + transform.forward * (_behaviour.Speed * Time.deltaTime);
    }

    public void TryGetComponentTile(RaycastHit hit)
    {
        
        if (_currentTile.Content.TileType == TypeOfTile.Destination)
        {
            PassedCounter.NotifyCounterOn?.Invoke(1);
            Die();
            return;
        }
        
        if (_currentTile.transform != hit.transform &&
            hit.transform.TryGetComponent(out Tile tile))
        {
            _currentTile = tile;
            PreviousDirection = _currentTile.Direction;
        }

        var transform1 = transform;
        transform1.position += transform1.forward * (_behaviour.Speed * Time.deltaTime);
        transform1.rotation = GetRotation();
    }
    
    public Quaternion GetRotation()
    {
        var speedRotation = SpeedRotation;
        if (PreviousDirection != _currentTile.Direction)
            speedRotation = 100f;
        
        return Quaternion.Lerp( transform.rotation,_currentTile.Direction.GetDirection(),speedRotation*Time.deltaTime*_behaviour.Speed);
    }

    private void Die()
    {
        if (_enemyView == null)
            GameManager.OnDestroy.AddListener(gameObject.Destroy);
        else
        {
            _enemyView.DieAnimation(this);
            GameManager.OnDestroy.AddListener(StartDestroy);
        }
        _addMoney((int)_behaviour.Price);
    }

    void IDamagable.TakeDamage(int damage)
    {
        if ((HealthPoints -= damage) <= 0)
            Die();
    }

    void ICollector.Recycle()
    {
        _onDestroy(this);
        IsTrigger = true;
    }

    private void StartDestroy()
    {
        GameManager.OnDestroy.RemoveListener(StartDestroy);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
        GameManager.OnDestroy.AddListener(gameObject.Destroy);
    }

    private void OnDestroy()
    {
        _onDestroy(this);
    }
}
