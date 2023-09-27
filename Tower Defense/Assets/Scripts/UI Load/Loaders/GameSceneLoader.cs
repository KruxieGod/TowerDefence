
    using System;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class GameSceneLoader : SceneProvider
    {
        public TilesCounterLoader TilesCounterLoader { get; private set; }
        public SelectingTilesLoader SelectingTilesLoader { get; private set; }
        public CounterMoneyLoader CounterMoneyLoader { get; private set; }
        
        public GameSceneLoader(GameObjectsProvider gameObjectsProvider,
            CounterMoneyLoader counterMoneyLoader,
            SelectingTilesLoader selectingTilesLoader,
            TilesCounterLoader tilesCounterLoader) : base(SceneData.GAMESCENE,gameObjectsProvider)
        {
            CounterMoneyLoader = counterMoneyLoader;
            SelectingTilesLoader = selectingTilesLoader;
            TilesCounterLoader = tilesCounterLoader;
        }

        public string Description { get; }
        // ReSharper disable Unity.PerformanceAnalysis
        public override async UniTask Load(Action<float> onProcess)
        {
            await base.Load(onProcess);
            await CounterMoneyLoader.Load(onProcess);
            await TilesCounterLoader.Load(onProcess);
            await SelectingTilesLoader.Load(onProcess);
        }
    }