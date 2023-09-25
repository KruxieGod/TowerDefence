
    using System;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class GameSceneLoader : SceneProvider
    {
        public TilesCounterLoader TilesCounterLoader { get; private set; }
        public SelectingTilesLoader SelectingTilesLoader { get; private set; }
        public CounterMoneyLoader CounterMoneyLoader { get; private set; }
        
        public GameSceneLoader(GameObjectsProvider gameObjectsProvider) : base(SceneData.GAMESCENE,gameObjectsProvider)
        {
        }

        public string Description { get; }
        // ReSharper disable Unity.PerformanceAnalysis
        public override async UniTask Load(Action<float> onProcess)
        {
            CounterMoneyLoader = new CounterMoneyLoader();
            //SelectingTilesLoader = new SelectingTilesLoader();
            TilesCounterLoader = new TilesCounterLoader();
            await base.Load(onProcess);
            await CounterMoneyLoader.Load(onProcess);
            await TilesCounterLoader.Load(onProcess);
            await SelectingTilesLoader.Load(onProcess);
        }
    }