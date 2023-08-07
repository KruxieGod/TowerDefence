using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BehaviourEnemy _behaviour;
    private Tile _currentTile;
    
    public void Initialize(BehaviourEnemy behaviour,Tile currentTile)
    {
        transform.position = currentTile.transform.position;
        _behaviour = behaviour;
        _currentTile = currentTile;
    }

    public void UpdatePos()
    {
        if (_currentTile.NextTile == null)
        {
            Destroy(gameObject);
            return;
        }
        if (Vector3.Distance(transform.position, _currentTile.NextTile.transform.position) <= 0.2f)
            _currentTile = _currentTile.NextTile;
        Debug.Log("UpdatePos");
        transform.position += _currentTile.transform.forward * _behaviour.Speed*Time.deltaTime;
    }
}
