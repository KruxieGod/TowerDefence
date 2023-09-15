using Cysharp.Threading.Tasks;

public class SecondTowerUpgradeVisitor : AssetLoader,ITowerUpgradeVisitor
{
    public UniTask<Turret<BehaviourBallistics> >VisitBallistics()
    {
        return LoadAsync<Turret<BehaviourBallistics>>(AddressableData.TowersData.BALLISTICS_SECOND_TOWER);
    }

    public UniTask<Turret<BehaviourTower>> VisitLaser()
    {
        return LoadAsync<Turret<BehaviourTower>>(AddressableData.TowersData.LASER_SECOND_TOWER);
    }

    public ITowerUpgradeVisitor NextVisitor() => null;
}