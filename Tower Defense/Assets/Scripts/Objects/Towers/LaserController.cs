using System;
using UnityEngine;

public class LaserController : TurretController<BehaviourTower>
{
    [SerializeField] private Laser _laser;
    protected override void LookAt()
    {
        transform.LookAt(_currentTarget.transform);
    }

    public override bool Shoot()
    {
        if (_currentTarget is not null)
        {
            _laser.StrenchTo(_currentTarget?.transform,0.2f);
            IDamagable damageTarget = _currentTarget;
            damageTarget?.TakeDamage((int)_damage);
        }
        return _currentTarget is not null;
    }
}