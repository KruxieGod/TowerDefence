
using Cysharp.Threading.Tasks;
using UnityEditor.VersionControl;

public class FirstTowerUpgradeVisitor : AssetLoader,ITowerUpgradeVisitor
{
    public Turret<BehaviourBallistics> VisitBallistics()
    {
        var task =  Load<Turret<BehaviourBallistics>>(AddressableData.TowersData.BALLISTICS_FIRST_TOWER).GetAwaiter();
        return task.WaitAndGetResult();
    }

    public Turret<BehaviourTower> VisitLaser()
    {
        var task =  Load<Turret<BehaviourTower>>(AddressableData.TowersData.LASER_FIRST_TOWER).GetAwaiter();
        return task.WaitAndGetResult();
    }

    public ITowerUpgradeVisitor NextVisitor() => null;
}