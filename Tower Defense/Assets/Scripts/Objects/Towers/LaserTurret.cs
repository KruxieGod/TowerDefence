
public class LaserTurret : Turret<BehaviourTower>
{
    public override TypeOfTile TileType => TypeOfTile.Laser;
    
    public override ITowerUpgradeVisitor NextVisitor()
    {
        throw new System.NotImplementedException();
    }
}