
using Cysharp.Threading.Tasks;

public interface ITowerUpgradeVisitor
{
    UniTask<Turret<BehaviourBallistics>> VisitBallistics();
    UniTask<Turret<BehaviourTower>> VisitLaser();
    ITowerUpgradeVisitor NextVisitor();
}