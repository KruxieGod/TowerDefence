
using Unity.VisualScripting;

public static class AddressableData
{
    public const string LOADINGSCREEN = "LoadingScreen";
    public const string GAMEFACTORIES = "GameFactory";
    public const string LEVELSCREEN = "LevelsScreen";
    public const string UI_TILES = "UITiles";
    public const string SELECTING_TILES_UI = "SelectingTilesUI";
    public const string UI_COUNTER_MONEY = "UI Counter Money";

    public static class TowersData
    {
        public static string LASER_FIRST_TOWER
        {
            get;
        } = "Laser First Tower";
        public const string BALLISTICS_FIRST_TOWER = "Ballistics First Tower";
        public const string LASER_SECOND_TOWER = "Laser Second Tower";
        public const string BALLISTICS_SECOND_TOWER = "Ballistics Second Tower";
    }
}

public static class SceneData
{
    public const string MAINMENUSCENE = "Main Menu";
    public const string GAMESCENE = "Game";
}

public static class PathCollection
{
    public const string PATHTOSCENARIOS = @"C:\Users\user\Documents\GitHub\TowerDefence\Tower Defense\Assets\Json Files\Scenarios\";
    public const string PATHTOSAVES = @"C:\Users\user\Documents\GitHub\TowerDefence\Tower Defense\Assets\Json Files\Saves\saves.json";
    public const string PATH_TO_TOWERS = @"C:\Users\user\Documents\GitHub\TowerDefence\Tower Defense\Assets\Json Files\TowerData\";
}