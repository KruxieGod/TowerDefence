using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class BulletBallista : MonoBehaviour,IProjectile
{
    [SerializeField] private ParticleSystem _effectExplosion;
    private float _radius;
    private float _damage;
    private float _speedBullet;
    private Rigidbody _rigidbody;
    private LayerMask _enemyLayer;
    // ReSharper disable Unity.PerformanceAnalysis
    public IProjectile Initialize(float radius,
        float damage,
        float speedBullet,
        LayerMask enemyLayer)
    {
        _enemyLayer = enemyLayer;
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
        var explosion = 
            Instantiate(
                _effectExplosion,
                transform.position,
                Quaternion.identity);
        var main = explosion.main;
        main.startSize = new ParticleSystem.MinMaxCurve(_radius*2f, _radius*2f);
        explosion.Play();
        Destroy(explosion, 3);
        foreach (IDamagable enemy in Physics.OverlapSphere(transform.position, _radius, _enemyLayer).GetEnemies())
            enemy.TakeDamage((int)_damage);
        GameManager.OnDestroy.AddListener(gameObject.Destroy);
    }
}