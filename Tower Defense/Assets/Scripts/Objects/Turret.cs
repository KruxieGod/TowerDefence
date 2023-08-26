using System;
using UnityEngine;

public class Turret : TileContent
{
    public LayerMask EnemyLayer { get; private set; }
    public override bool IsEnded => true;
    private BehaviourTower _behaviourTower;
    [SerializeField] private TurretController _turret;
    private float _lastToShoot;
    
    public Turret Initialize(BehaviourTower behaviourTower,LayerMask layerEnemy)
    {
        _behaviourTower = behaviourTower;
        _turret.Initialize(this,behaviourTower);
        _lastToShoot = _behaviourTower.SpeedFire;
        EnemyLayer = layerEnemy;
        return this;
    }

    public void TowerUpdate()
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
