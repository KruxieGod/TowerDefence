
public interface ITowerUpgradeVisitor
{
    Turret<BehaviourBallistics> VisitBallistics();
    Turret<BehaviourTower> VisitLaser();
    ITowerUpgradeVisitor NextVisitor();
}