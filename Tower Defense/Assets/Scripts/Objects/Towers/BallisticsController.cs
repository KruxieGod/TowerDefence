using UnityEngine;

public class BallisticsController : TurretController<BehaviourBallistics>
{
    [SerializeField] private BulletBallista _prefabBullet;
    [SerializeField] private Transform _root;
    private readonly float g = Physics.gravity.y;
    private float _speed;
    protected override void LookAt()
    {
        if (_currentTarget is null)
            return;
        var position = _currentTarget.transform.position;
        _root.localRotation = Quaternion.Euler(new Vector3(0,Quaternion.LookRotation(_root.position - position).eulerAngles.y,0));
        _speed = CalculateSpeed(position + _currentTarget.transform.forward);
        var localEulerAngles = transform.localEulerAngles;
        localEulerAngles = new Vector3(_behaviourTower.AngleBullet,localEulerAngles.y,localEulerAngles.z);
        transform.localEulerAngles = localEulerAngles;
    }

    public override bool Shoot()
    {
        if (_currentTarget is null)
            return false;
        Instantiate(_prefabBullet,
                transform.position,
                Quaternion.identity)
            .Initialize(_behaviourTower.GetBulletInfo(),
                _turret.EnemyLayer)
            .Launch(-transform.forward,_speed);
        return true;
    }

    private float CalculateSpeed(Vector3 position)
    {
        Vector3 fromTo = position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = _behaviourTower.AngleBullet * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));
        return v;
    }
}
