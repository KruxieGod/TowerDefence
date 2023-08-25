using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameTowerFactory : BaseFactoryCollection<Tourrel>
{
    [SerializeField] private BehaviourTower _behaviourTower;
    [SerializeField] private LayerMask _layerEnemy;
    protected override Tourrel Initialization(Tourrel prefab)
    {
        return prefab.Initialize(_behaviourTower,_layerEnemy);
    }
}
