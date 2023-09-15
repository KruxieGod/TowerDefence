
public class LaserTurret : Turret<BehaviourTower>
{
    public override TypeOfTile TileType => TypeOfTile.Laser;
    protected override async void UpgradeTower()
    {
        _visitor = _visitor.NextVisitor();
        ((ISetterTile)SpawnerTile).SetContentTile((await _visitor.VisitLaser()).Initialize(this));
        Destroy(gameObject);
    }
}