using UnityEngine;

[RequireComponent(typeof(EventTriggerButton), typeof(Collider))]
public abstract class Turret : TileContent
{
    public abstract ITowerUpgradeVisitor NextVisitor();
}