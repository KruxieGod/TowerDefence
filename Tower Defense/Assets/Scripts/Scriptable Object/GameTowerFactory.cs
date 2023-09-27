using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu]
public class GameTowerFactory : BaseFactoryCollection<IUpdatable>
{
    [SerializeField] private LaserTurret _laserTurretPrefab;
    [SerializeField] private Ballista _ballistaPrefab;
    [Inject] private TowerInfoLoader _towerInfoLoader;
    [Inject] private CounterMoney _counterMoney;
    public Turret<BehaviourBallistics> GetBallista() => GetPrefab(_ballistaPrefab);
    public Turret<BehaviourTower> GetLaserTurret() => GetPrefab(_laserTurretPrefab);

    private Turret<T> GetPrefab<T>(Turret<T> prefab)
        where T : BehaviourTower
    {
        var pref = Instantiate(prefab).Initialize( this,
            _towerInfoLoader,
            _counterMoney.EnoughMoney,
            new FirstTowerUpgradeVisitor());
        return pref;
    }

    public void Remove(IUpdatable tower) => _data.Remove(tower);
    public void Add(IUpdatable tower) => _data.Add(tower);
}
