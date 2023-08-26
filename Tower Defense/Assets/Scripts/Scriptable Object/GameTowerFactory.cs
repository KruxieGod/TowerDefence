using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameTowerFactory : BaseFactoryCollection<Turret>
{
    [SerializeField] private Turret _turretPrefab;
    [SerializeField] private BehaviourTower _behaviourTower;
    [SerializeField] private LayerMask _layerEnemy;
    protected override Turret Initialization(Turret prefab)
    {
        return prefab.Initialize(_behaviourTower,_layerEnemy);
    }

    public Turret GetTurret() => GetPrefab(_turretPrefab);
}
