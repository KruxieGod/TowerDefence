using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BehaviourEnemy
{
    [SerializeField,FloatRangeSlider(0.5f,15f)]private FloatRange _speed;
    [SerializeField,FloatRangeSlider(1,300)]private FloatRange _hp;
    public float Speed => _speed.RandomValueInRange;
    public int HP => (int)_hp.RandomValueInRange;
}