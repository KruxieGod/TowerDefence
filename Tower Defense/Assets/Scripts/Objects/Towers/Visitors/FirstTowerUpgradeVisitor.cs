
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.VersionControl;

public class FirstTowerUpgradeVisitor : AssetLoader,ITowerUpgradeVisitor
{
    public UniTask<Turret<BehaviourBallistics>> VisitBallistics()
    {
        return LoadAsync<Turret<BehaviourBallistics>>(AddressableData.TowersData.BALLISTICS_FIRST_TOWER);;
    }

    public UniTask<Turret<BehaviourTower>> VisitLaser()
    {
        return LoadAsync<Turret<BehaviourTower>>(AddressableData.TowersData.LASER_FIRST_TOWER);
    }

    public ITowerUpgradeVisitor NextVisitor() => new SecondTowerUpgradeVisitor();
}