using System.Linq;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Enemy _currentTarget;
    private float _radius;
    private float _damage;
    private Tourrel _tourrel;
    [SerializeField] private Laser _laser;
    
    public void Initialize(Tourrel tourrel)
    {
        _radius = tourrel.BehaviourTower.Radius;
        _damage = tourrel.BehaviourTower.Damage;
        _tourrel = tourrel;
    }
    
    public void PursueTarget()
    {
        if (_currentTarget != null &&
            Vector3.Distance(_currentTarget.transform.position, _tourrel.transform.position) <= _radius)
            transform.LookAt(_currentTarget.transform);
        else
            FindTarget();
    }

    private void FindTarget()
    {
        if (_tourrel == null)
            return;
        _currentTarget = Physics.OverlapSphere(_tourrel.transform.position, _radius,_tourrel.EnemyLayer)
            .Select(x => x.transform.root.GetComponent<Enemy>())
            .FirstOrDefault(enemy => enemy != null);
    }

    public bool Shoot()
    {
        if (_currentTarget != null)
        {
            _laser.StrenchTo(_currentTarget.transform,0.2f);
            ((IDamagable)_currentTarget).TakeDamage((int)_damage);
        }
        
        return _currentTarget != null;
    }
}
