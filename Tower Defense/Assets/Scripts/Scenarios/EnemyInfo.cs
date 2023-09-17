using System;
using UnityEngine;

[Serializable]
public struct EnemyInfo
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField,FloatRangeSlider(0.5f,20f)]private FloatRange _speed;
    [SerializeField,FloatRangeSlider(1,300)]private FloatRange _hp;
    [SerializeField,FloatRangeSlider(1,200)]private FloatRange _cost;
    public float Speed => _speed.RandomValueInRange;
    public float Price => _cost.RandomValueInRange;
    public int HP => (int)_hp.RandomValueInRange;
    public EnemyType EnemyType => _enemyType;

    public EnemyInfo OnValidate()
    {
        _speed = _speed.ClampSpeed(_enemyType);
        _hp = _hp.ClampHP(_enemyType);
        _cost = _cost.ClampPrice(_enemyType);
        return this;
    }
}