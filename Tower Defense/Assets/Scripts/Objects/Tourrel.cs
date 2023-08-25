using System;
using UnityEngine;

public class Tourrel : TileContent
{
    public LayerMask EnemyLayer { get; private set; }
    public override bool IsEnded => true;
    public BehaviourTower BehaviourTower { get; private set; }
    [SerializeField] private TurretController _turret;
    private float _lastToShoot;
    
    public Tourrel Initialize(BehaviourTower behaviourTower,LayerMask layerEnemy)
    {
        BehaviourTower = behaviourTower;
        _turret.Initialize(this);
        _lastToShoot = BehaviourTower.SpeedFire;
        EnemyLayer = layerEnemy;
        return this;
    }

    public void TowerUpdate()
    {
        if (_lastToShoot <= 0)
            _lastToShoot = BehaviourTower.SpeedFire;
        else
            _turret.Shoot();
        _lastToShoot -= Time.deltaTime;
        _turret.PursueTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,BehaviourTower.Radius);
    }
}
