using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class TilesCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wall;
    [SerializeField] private TextMeshProUGUI _destination;
    [SerializeField] private TextMeshProUGUI _mortar;
    [SerializeField] private TextMeshProUGUI _laser;

    private void Awake() =>
        GetComponent<Canvas>().worldCamera = ProjectContexter.Instance.UiCamera;

    public TilesCounterUI Initialize(CountTiles countTiles)
    {
        Set(TypeOfTile.Wall,countTiles.Walls);
        Set(TypeOfTile.Destination,countTiles.Destinations);
        Set(TypeOfTile.Laser,countTiles.Lasers);
        Set(TypeOfTile.Mortar,countTiles.Mortars);
        return this;
    }
    
    public void Set(TypeOfTile type, int count)
    {
        switch (type)
        {
            case TypeOfTile.Wall:
                _wall.SetText(count.ToString());
                break;
            case TypeOfTile.Mortar:
                _mortar.SetText(count.ToString());
                break;
            case TypeOfTile.Laser:
                _laser.SetText(count.ToString());
                break;
            case TypeOfTile.Destination:
                _destination.SetText(count.ToString());
                break;
        }
    }
}
