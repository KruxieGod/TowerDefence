using Cysharp.Threading.Tasks;

public class SecondTowerUpgradeVisitor : AssetLoader,ITowerUpgradeVisitor
{
    public async UniTask<Turret<BehaviourBallistics> >VisitBallistics()
    {
        var obj = await LoadAsync<Turret<BehaviourBallistics>>(AddressableData.TowersData.BALLISTICS_SECOND_TOWER);
        obj.SetBehaviour(new BehaviourBallistics(1.5f, 45f, 25f, 6f,35f));
        return obj;
    }

    public async UniTask<Turret<BehaviourTower>> VisitLaser()
    {
        var obj = await LoadAsync<Turret<BehaviourTower>>(AddressableData.TowersData.LASER_SECOND_TOWER);
        obj.SetBehaviour(new BehaviourTower(0.3f, 5f, 20));
        return obj;
    }

    public ITowerUpgradeVisitor NextVisitor() => null;
}