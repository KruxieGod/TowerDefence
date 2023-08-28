using System;
using UnityEngine;

[Serializable]
public class BehaviourBallistics : BehaviourTower
{
    [SerializeField] private float _radiusDamage;
    public float RadiusDamage => _radiusDamage;
    
    [SerializeField,Min(15f)] private float _speedBullet;
    public float SpeedBullet => _speedBullet;
    public BehaviourBallistics(float speedFire, float damage, float radius,float radiusDamage,float speedBullet) : base(speedFire, damage, radius)
    {
        _radiusDamage = radiusDamage;
    }
}