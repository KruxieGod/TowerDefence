using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticsController : TurretController<BehaviourBallistics>
{
    [SerializeField] private BulletBallista _prefabBullet;
    [SerializeField] private Transform _root;
    protected override void LookAt()
    {
        var position = _currentTarget.transform.position;
        _root.localRotation = Quaternion.Euler(new Vector3(0,Quaternion.LookRotation(_root.position - position).eulerAngles.y,0));
        float angle = CalculateAngle(position);
        transform.localRotation = Quaternion.Euler(new Vector3(360f - angle,0,0));
    }

    public override bool Shoot()
    {
        return _currentTarget is not null;
    }

    private float CalculateAngle(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        float y = direction.y;
        direction.y = 0;
        float x = direction.magnitude;
        float speedSqr = _behaviourTower.SpeedBullet * _behaviourTower.SpeedBullet;
        float underTheSqrRoot =
            (speedSqr * speedSqr) - Physics.gravity.y * (Physics.gravity.y * x * x + 2 * y * speedSqr);
        if (underTheSqrRoot >= 0)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            Debug.Log(Mathf.Atan2(speedSqr + root, Physics.gravity.y)*Mathf.Rad2Deg);
            return Mathf.Atan2(speedSqr + root, Physics.gravity.y)*Mathf.Rad2Deg;
        }
        return 0f;
    }
}
