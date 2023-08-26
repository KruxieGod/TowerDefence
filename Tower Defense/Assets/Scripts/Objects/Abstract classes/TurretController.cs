using System.Linq;
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
        if (_currentTarget != null &&
            Vector3.Distance(_currentTarget.transform.position, _turret.transform.position) <= _radius)
            LookAt();
        else
            FindTarget();
    }

    protected abstract void LookAt();
    
    private void FindTarget()
    {
        if (_turret == null)
            return;
        _currentTarget = Physics.OverlapSphere(_turret.transform.position, _radius,_turret.EnemyLayer)
            .Select(x => x.transform.root.GetComponent<Enemy>())
            .FirstOrDefault(enemy => enemy != null);
    }

    public abstract bool Shoot();
}
