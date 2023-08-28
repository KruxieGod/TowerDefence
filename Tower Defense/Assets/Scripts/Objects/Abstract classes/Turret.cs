using System;
using UnityEngine;

public abstract class Turret<BehaviourT> : TileContent,IUpdatable
    where BehaviourT : BehaviourTower
{
    public override TypeOfTile TileType => TypeOfTile.Turret;
    [SerializeField] private LayerMask _enemyLayer;
    public LayerMask EnemyLayer => _enemyLayer;
    private BehaviourTower _behaviourTower;
    [SerializeField] private TurretController<BehaviourT> _turret;
    private float _lastToShoot;
    
    public Turret<BehaviourT> Initialize(BehaviourT behaviourTower)
    {
        _behaviourTower = behaviourTower;
        _turret.Initialize(this,behaviourTower);
        _lastToShoot = _behaviourTower.SpeedFire;
        return this;
    }

    void IUpdatable.UpdateEntity()
    {
        _turret?.PursueTarget();
        if (_lastToShoot <= 0)
            _lastToShoot = _turret.Shoot() ? _behaviourTower.SpeedFire : 0;
        _lastToShoot -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,_behaviourTower.Radius);
    }
}
