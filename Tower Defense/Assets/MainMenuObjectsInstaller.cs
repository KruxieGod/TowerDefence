using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuObjectsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Instantiate<LevelsScreen>();
    }
}
