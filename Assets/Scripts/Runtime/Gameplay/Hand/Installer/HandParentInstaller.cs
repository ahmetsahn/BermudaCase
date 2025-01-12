using Runtime.Core.Interface;
using Runtime.Gameplay.Hand.Controller;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Installer
{
    public class HandParentInstaller : MonoInstaller
    {
        [SerializeField]
        private HandParentModelConfig handParentModelConfig;

        public override void InstallBindings()
        {
            Container.Bind<HandParentView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<HandParentModel>().AsSingle().WithArguments(handParentModelConfig);

            Container.BindInterfacesTo<HandParentMovementController>().AsSingle();
            Container.BindInterfacesTo<HandParentPushRateBuff>().AsSingle();
            Container.BindInterfacesTo<HandParentWidthBuffController>().AsSingle();
            Container.BindInterfacesTo<HandParentLengthBuffController>().AsSingle();
        }
    }
}