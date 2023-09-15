
    using System;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class GameSceneLoader : SceneProvider
    {
        public TilesCounterUILoader TilesCounterUILoader { get; private set; }
        public SelectingTilesLoader SelectingTilesLoader { get; private set; }
        
        public GameSceneLoader() : base(SceneData.GAMESCENE)
        {
        }

        public string Description { get; }
        public override async UniTask Load(Action<float> onProcess)
        {
            SelectingTilesLoader = new SelectingTilesLoader();
            TilesCounterUILoader = new TilesCounterUILoader();
            await base.Load(onProcess);
            await TilesCounterUILoader.Load(onProcess);
            await SelectingTilesLoader.Load(onProcess);
        }
    }