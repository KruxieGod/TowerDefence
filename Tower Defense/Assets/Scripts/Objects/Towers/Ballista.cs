
public class Ballista : Turret<BehaviourBallistics>
{
    public override TypeOfTile TileType => TypeOfTile.Mortar;
    
    public override ITowerUpgradeVisitor NextVisitor()
    {
        throw new System.NotImplementedException();
    }
}