using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class BehaviourBallistics : BehaviourTower
{
    [SerializeField] private float _radiusDamage;
    public float RadiusDamage => _radiusDamage;
    
    [SerializeField] private float _angleBullet;
    public float AngleBullet => _angleBullet;
    public BehaviourBallistics(float speedFire, float damage, float radius,float radiusDamage,float angleBullet) : base(speedFire, damage, radius)
    {
        _radiusDamage = radiusDamage;
        _angleBullet = angleBullet;
    }

    public BulletInfo GetBulletInfo() => new BulletInfo(this);
}

public struct BulletInfo
{
    public readonly float RadiusDamage;
    public readonly float Damage;
    public readonly float AngleBullet;

    public BulletInfo(BehaviourBallistics behaviourBallistics)
    {
        RadiusDamage = behaviourBallistics.RadiusDamage;
        Damage = behaviourBallistics.Damage;
        AngleBullet = behaviourBallistics.AngleBullet;
    }
}