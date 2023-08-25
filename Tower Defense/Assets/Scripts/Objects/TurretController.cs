using System.Linq;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Enemy _currentTarget;
    private float _radius;
    private float _damage;
    private Tourrel _tourrel;
    
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
        _currentTarget = Physics.OverlapSphere(_tourrel.transform.position, _radius,_tourrel.EnemyLayer)
            .Select(x => x.transform.root.GetComponent<Enemy>())
            .FirstOrDefault(enemy => enemy != null);
    }

    public void Shoot()
    {
        
    }
}
