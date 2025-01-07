using Runtime.Core.Interface;
using Runtime.Gameplay.Hand.Controller;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.InputHandler;
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
            
            Container.BindInterfacesTo<HandMovementController>().AsSingle();
            Container.BindInterfacesTo<HandInputController>().AsSingle();
            
#if UNITY_ANDROID || UNITY_IOS
            Container.Bind<IInputHandler>().To<TouchInputHandler>().AsSingle();
#else
            Container.Bind<IInputHandler>().To<MouseInputHandler>().AsSingle();
#endif
        }
    }
}