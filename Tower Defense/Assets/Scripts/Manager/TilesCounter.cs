
using System.Collections.Generic;

public class TilesCounter
{
    private CountTiles _countTiles;
    private Dictionary<TypeOfTile, int> _typesCount;
    private TilesCounterUI _tilesCounterUI => ProjectContext.Instance.GameSceneLoader.TilesCounterLoader?.TilesCounterUI;
    public TilesCounter(CountTiles countTiles)
    {
        _countTiles = countTiles;
        _typesCount = new Dictionary<TypeOfTile, int>()
        {
            { TypeOfTile.Destination , _countTiles.Destinations},
            { TypeOfTile.Wall , _countTiles.Walls},
            { TypeOfTile.Mortar , _countTiles.Mortars},
            { TypeOfTile.Laser , _countTiles.Lasers}
        };
    }

    public void Reset()
    {
        _typesCount = new Dictionary<TypeOfTile, int>()
        {
            { TypeOfTile.Destination , _countTiles.Destinations},
            { TypeOfTile.Wall , _countTiles.Walls},
            { TypeOfTile.Mortar , _countTiles.Mortars},
            { TypeOfTile.Laser , _countTiles.Lasers}
        };
        if (_tilesCounterUI != null)
             _tilesCounterUI.Initialize(_countTiles);
    }
    
    public bool TryPlace(TypeOfTile type)
    {
        if (!_typesCount.ContainsKey(type))
            return true;
        if (_typesCount[type] - 1 < 0) 
            return false;
        _typesCount[type]--;
        _tilesCounterUI.Set(type,_typesCount[type]);
        return true;

    }
    
    public void Replace(TypeOfTile type)
    {
        if (!_typesCount.ContainsKey(type))
            return;
        _typesCount[type]++;
        _tilesCounterUI.Set(type,_typesCount[type]);
    }
}