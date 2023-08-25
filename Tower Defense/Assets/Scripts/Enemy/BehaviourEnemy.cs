using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BehaviourEnemy
{
    public readonly float Speed;
    public readonly int HP;
    public BehaviourEnemy(float speed,int hp)
    {
        Speed = speed;
        HP = hp;
    }
}

[Serializable]
public class BehaviourTower
{
    [SerializeField]private float _speedFire;
    [SerializeField]private float _damage;
    [SerializeField]private float _radius;

    public float SpeedFire => _speedFire;
    public float Damage => _damage;
    public float Radius => _radius;

    public BehaviourTower(float  speedFire,float  damage, float radius)
    {
        _speedFire = speedFire;
        _damage = damage;
        _radius = radius;
    }
}
