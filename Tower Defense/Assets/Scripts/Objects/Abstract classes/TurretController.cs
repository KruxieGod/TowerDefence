using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TurretController<Behaviour> : MonoBehaviour
where Behaviour : BehaviourTower
{
    protected Enemy _currentTarget;
    private float _radius;
    protected float _damage;
    protected Turret<Behaviour> _turret;
    protected Behaviour _behaviourTower;
    public void Initialize(Turret<Behaviour> turret, Behaviour behaviourTower) 
    {
        _radius = behaviourTower.Radius;
        _damage = behaviourTower.Damage;
        _turret = turret;
        _behaviourTower = behaviourTower;
    }
    
    public void PursueTarget()
    {
        if (_currentTarget.IsUnityNull() ||
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
                                     Vector3.Distance(enemy.transform.position, _turret.transform.position) <= _radius);
    }

    public abstract bool Shoot();
}
