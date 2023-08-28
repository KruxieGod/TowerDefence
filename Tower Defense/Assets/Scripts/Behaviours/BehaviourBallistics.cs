using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class BehaviourBallistics : BehaviourTower
{
    [SerializeField] private float _radiusDamage;
    public float RadiusDamage => _radiusDamage;
    
    [SerializeField,Min(14f)] private float _speedBullet;
    public float SpeedBullet => _speedBullet;
    public BehaviourBallistics(float speedFire, float damage, float radius,float radiusDamage,float speedBullet) : base(speedFire, damage, radius)
    {
        _radiusDamage = radiusDamage;
    }

    public BulletInfo GetBulletInfo() => new BulletInfo(this);
}

public struct BulletInfo
{
    public readonly float RadiusDamage;
    public readonly float Damage;
    public readonly float SpeedBullet;

    public BulletInfo(BehaviourBallistics behaviourBallistics)
    {
        RadiusDamage = behaviourBallistics.RadiusDamage;
        Damage = behaviourBallistics.Damage;
        SpeedBullet = behaviourBallistics.SpeedBullet;
    }
}