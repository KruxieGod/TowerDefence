using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class BulletBallista : MonoBehaviour,IProjectile
{
    [SerializeField] private ParticleSystem _effectExplosion;
    private BulletInfo _bulletInfo;
    private Rigidbody _rigidbody;
    private LayerMask _enemyLayer;
    // ReSharper disable Unity.PerformanceAnalysis
    public IProjectile Initialize(BulletInfo bulletInfo,
        LayerMask enemyLayer)
    {
        _bulletInfo = bulletInfo;
        _enemyLayer = enemyLayer;
        _rigidbody = GetComponent<Rigidbody>();
        return this;
    }

    void IProjectile.Launch(Vector3 forward,float speed)
    {
        _rigidbody.velocity = forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        var explosion = 
            Instantiate(
                _effectExplosion,
                transform.position,
                Quaternion.identity);
        var main = explosion.main;
        main.startSize = new ParticleSystem.MinMaxCurve(_bulletInfo.RadiusDamage, _bulletInfo.RadiusDamage);
        explosion.Play();
        Destroy(explosion.gameObject, 3);
        foreach (IDamagable enemy in Physics.OverlapSphere(transform.position, _bulletInfo.RadiusDamage, _enemyLayer).GetEnemies())
            enemy.TakeDamage((int)_bulletInfo.Damage);
        gameObject.Destroy();
    }
}