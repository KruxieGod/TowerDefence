using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TurretController<TBehaviour> : MonoBehaviour
where TBehaviour : BehaviourTower
{
    protected Enemy _currentTarget;
    private float _radius;
    protected float _damage;
    protected Turret<TBehaviour> _turret;
    protected TBehaviour _behaviourTower;
    public void Initialize(Turret<TBehaviour> turret, TBehaviour behaviourTower) 
    {
        _radius = behaviourTower.Radius;
        _damage = behaviourTower.Damage;
        _turret = turret;
        _behaviourTower = behaviourTower;
    }
    
    public void PursueTarget()
    {
        if (_currentTarget.IsUnityNull() ||
            _currentTarget.IsTrigger ||
            Vector3.Distance(_currentTarget.transform.position, _turret.transform.position) > _radius)
            FindTarget();
        LookAt();
    }

    protected abstract void LookAt();
    
    private void FindTarget()
    {
        _currentTarget = Physics.OverlapSphere(_turret.transform.position, _radius,_turret.EnemyLayer)
            .GetEnemies()
            .FirstOrDefault(enemy => enemy != null &&
                                     !enemy.IsTrigger && 
                                     Vector3.Distance(enemy.transform.position, _turret.transform.position) <= _radius);
    }

    public abstract bool Shoot();
}
