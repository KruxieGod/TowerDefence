using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[SelectionBase,RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour,IDamagable,ICollector
{
    public bool IsTrigger { get; private set; }
    private EnemyView _enemyView;
    public const float SpeedRotation = 0.6f;
    [field: SerializeField] public LayerMask LayerFloor { get; private set; }
    private EnemyInfo _behaviour;
    private Tile _currentTile;
    public const float RADIUS = GameBoard.POSITIONMULTIPLIER/2;
    private Direction _previousDirection;
    public const float Distance = 1f;
    public int HealthPoints { get; private set; }
    private Action<Enemy> _onDestroy;
    public float Speed => _behaviour.Speed;
    public void Initialize(
        Tile currentTile,
        Action<Enemy> onDestroy)
    {
        _enemyView = GetComponentInChildren<EnemyView>();
        _onDestroy = onDestroy;
        _previousDirection = currentTile.Direction;
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

    public TransformInfo GetInfos(Vector3 posEnemy,Vector3 forwardEnemy,Quaternion rotationEnemy) => 
        new (position:GetDirection(posEnemy,forwardEnemy),rotation: GetRotation(rotationEnemy));
    
    public void UpdatePos()
    {
        //transform.position = GetDirection(transform);
        //transform.rotation = GetRotation(transform);
    }

    public Vector3 GetDirection(Vector3 positionEnemy,Vector3 forwardEnemy)
    {
        if (_currentTile.Content.TileType == TypeOfTile.Destination)
        {
            PassedCounter.NotifyCounterOn?.Invoke(1);
            Die();
        }
        if (Physics.Raycast (new Vector3(positionEnemy.x,positionEnemy.y+1f,positionEnemy.z),
                Vector3.down, out var hit,Distance,LayerFloor)
            && _currentTile.transform != hit.transform
            && hit.transform.TryGetComponent(out Tile tile))
        {
            _currentTile = tile;
            _previousDirection = _currentTile.Direction;
        }

        return positionEnemy + forwardEnemy * (_behaviour.Speed * Time.deltaTime);
    }
    
    public Quaternion GetRotation(Quaternion rotationEnemy)
    {
        float speedRotation = SpeedRotation/2f;

        if (_previousDirection != _currentTile.Direction)
            speedRotation = 100f;
        _previousDirection = _currentTile.Direction;
        return Quaternion.Lerp( rotationEnemy,_currentTile.Direction.GetDirection(),speedRotation*Time.deltaTime*_behaviour.Speed);
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
        ProjectContext.Instance.GameSceneLoader.CounterMoneyLoader.CounterMoney.AddMoney((int)_behaviour.Price);
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
