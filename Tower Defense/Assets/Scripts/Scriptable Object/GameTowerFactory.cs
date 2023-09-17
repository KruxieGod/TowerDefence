using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameTowerFactory : BaseFactoryCollection<IUpdatable>
{
    [SerializeField] private LaserTurret _laserTurretPrefab;
    [SerializeField] private Ballista _ballistaPrefab;
    [SerializeField] private BehaviourTower _behaviourTower;
    [SerializeField] private BehaviourBallistics _behaviourBallistics;

    public Turret<BehaviourBallistics> GetBallista() => GetPrefab(_ballistaPrefab,_behaviourBallistics);
    public Turret<BehaviourTower> GetLaserTurret() => GetPrefab(_laserTurretPrefab,_behaviourTower);

    private Turret<T> GetPrefab<T>(Turret<T> prefab,T behaviourTower)
        where T : BehaviourTower
    {
        var pref = Instantiate(prefab).Initialize( behaviourTower,this,new FirstTowerUpgradeVisitor());
        return pref;
    }

    public void Remove(IUpdatable tower) => _data.Remove(tower);
    public void Add(IUpdatable tower) => _data.Add(tower);
}
