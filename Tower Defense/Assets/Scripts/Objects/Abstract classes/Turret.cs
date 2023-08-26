using System;
using UnityEngine;

public abstract class Turret<BehaviourT> : TileContent,IUpdatable
    where BehaviourT : BehaviourTower
{
    public override TypeOfTile TileType => TypeOfTile.Turret;
    public LayerMask EnemyLayer { get; private set; }
    private BehaviourTower _behaviourTower;
    [SerializeField] private TurretController<BehaviourT> _turret;
    private float _lastToShoot;
    
    public Turret<BehaviourT> Initialize(BehaviourT behaviourTower,LayerMask layerEnemy)
    {
        _behaviourTower = behaviourTower;
        _turret.Initialize(this,behaviourTower);
        _lastToShoot = _behaviourTower.SpeedFire;
        EnemyLayer = layerEnemy;
        return this;
    }

    void IUpdatable.UpdateEntity()
    {
        if (_lastToShoot <= 0)
            _lastToShoot = _turret.Shoot() ? _behaviourTower.SpeedFire : 0;
        _lastToShoot -= Time.deltaTime;
        _turret.PursueTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,_behaviourTower.Radius);
    }
}
