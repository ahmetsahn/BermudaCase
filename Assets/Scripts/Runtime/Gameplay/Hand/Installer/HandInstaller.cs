using System;
using Runtime.Gameplay.Hand.Controller;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Installer
{
    public class HandInstaller : MonoInstaller
    {
        [SerializeField]
        private HandModelConfig handModelConfig;
        public override void InstallBindings()
        {
            Container.Bind<HandView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<HandModel>().AsSingle().WithArguments(handModelConfig);
            Container.BindInterfacesTo<HandCollisionController>().AsSingle();
        }
    }
}