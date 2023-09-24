
using System;
using UnityEngine;

[Serializable]
public class TowerData<TBehaviour>
where TBehaviour : BehaviourTower
{
    [field : SerializeField] public string NameTower { get; private set; }
    [field : SerializeField] public int Price { get; private set; }
    [field : SerializeField] public TBehaviour BehaviourTower { get; private set; }
    public TowerData(string nameTower,int price,TBehaviour behaviourTower)
    {
        NameTower = nameTower;
        Price = price;
        BehaviourTower = behaviourTower;
    }
}