using UnityEngine;

public class LaserController : TurretController
{
    [SerializeField] private Laser _laser;
    protected override void LookAt()
    {
        transform.LookAt(_currentTarget.transform);
    }

    public override bool Shoot()
    {
        if (_currentTarget != null)
        {
            _laser.StrenchTo(_currentTarget.transform,0.2f);
            ((IDamagable)_currentTarget).TakeDamage((int)_damage);
        }
        return _currentTarget != null;
    }
}