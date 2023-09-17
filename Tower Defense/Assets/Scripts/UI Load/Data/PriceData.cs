
using System;
using UnityEngine;

[Serializable]
public class PriceData
{
    [field : SerializeField] public string NameTower { get; private set; }
    [field : SerializeField] public int Price { get; private set; }

    public PriceData(string nameTower,int price)
    {
        NameTower = nameTower;
        Price = price;
    }
}