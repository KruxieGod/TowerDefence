using Cysharp.Threading.Tasks;

public class SecondTowerUpgradeVisitor : AssetLoader,ITowerUpgradeVisitor
{
    public async UniTask<Turret<BehaviourBallistics> >VisitBallistics()
    {
        var obj = await LoadAsync<Turret<BehaviourBallistics>>(AddressableData.TowersData.BALLISTICS_SECOND_TOWER);
        return obj;
    }

    public async UniTask<Turret<BehaviourTower>> VisitLaser()
    {
        var obj = await LoadAsync<Turret<BehaviourTower>>(AddressableData.TowersData.LASER_SECOND_TOWER);
        return obj;
    }

    public ITowerUpgradeVisitor NextVisitor() => null;
}