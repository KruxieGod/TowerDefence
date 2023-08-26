using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticsController : TurretController
{
    protected override void LookAt()
    {
        transform.LookAt(_currentTarget.transform);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 45));
    }

    public override bool Shoot()
    {
        return _currentTarget != null;
    }
}
