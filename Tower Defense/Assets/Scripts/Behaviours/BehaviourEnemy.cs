using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BehaviourEnemy
{
    [SerializeField]private float _speed;
    [SerializeField]private int _hp;
    public float Speed => _speed;
    public int HP => _hp;
}