using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Turret<BehaviourT> :  Turret,IUpdatable
    where BehaviourT : BehaviourTower
{
    [SerializeField] private LayerMask _enemyLayer;
    public LayerMask EnemyLayer => _enemyLayer;
    private BehaviourTower _behaviourTower;
    [SerializeField] private TurretController<BehaviourT> _turret;
    private float _lastToShoot;
    private GameTowerFactory _towerFactory;
    private ITowerUpgradeVisitor _visitor;
    
    public Turret<BehaviourT> Initialize(BehaviourT behaviourTower,
        GameTowerFactory towerFactory,
        ITowerUpgradeVisitor visitor)
    {
        _visitor = visitor;
        _towerFactory = towerFactory;
        _behaviourTower = behaviourTower;
        _turret.Initialize(this,behaviourTower);
        _lastToShoot = _behaviourTower.SpeedFire;
        return this;
    }

    private void OnEnable() => _turret.enabled = true;

    private void OnDisable() => _turret.enabled = false;

    void IUpdatable.UpdateEntity()
    {
        if (!_turret.enabled)
            return;
        _turret?.PursueTarget();
        if (_lastToShoot <= 0)
            _lastToShoot = _turret.Shoot() ? _behaviourTower.SpeedFire : 0;
        _lastToShoot -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        _towerFactory.Remove(this);
    }
}
