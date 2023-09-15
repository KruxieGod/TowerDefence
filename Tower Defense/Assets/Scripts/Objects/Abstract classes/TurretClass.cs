using UnityEngine;

[RequireComponent(typeof(EventTriggerButton), typeof(Collider))]
public abstract class Turret : TileContent
{
    protected abstract void UpgradeTower();
}