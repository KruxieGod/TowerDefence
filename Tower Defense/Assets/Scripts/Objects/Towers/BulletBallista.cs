using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class BulletBallista : MonoBehaviour,IProjectile
{
    private float _radius;
    private float _damage;
    private float _speedBullet;
    private Rigidbody _rigidbody;

    // ReSharper disable Unity.PerformanceAnalysis
    public IProjectile Initialize(float radius, float damage,float speedBullet)
    {
        _radius = radius;
        _damage = damage;
        _speedBullet = speedBullet;
        _rigidbody = GetComponent<Rigidbody>();
        return this;
    }

    void IProjectile.Launch(Vector3 forward)
    {
        _rigidbody.velocity = forward * _speedBullet;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}