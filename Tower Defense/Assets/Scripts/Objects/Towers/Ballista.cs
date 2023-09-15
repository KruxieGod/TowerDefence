
public class Ballista : Turret<BehaviourBallistics>
{
    public override TypeOfTile TileType => TypeOfTile.Mortar;
    protected override async void UpgradeTower()
    {
        _visitor = _visitor.NextVisitor();
        ((ISetterTile)SpawnerTile).SetContentTile((await _visitor.VisitBallistics()).Initialize(_behaviourTower,_towerFactory,_visitor));
        Destroy(gameObject);
    }
}