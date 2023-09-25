using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private ProjectContexter _projectContexter;
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(_projectContexter.UiCamera);
    }
}
