using Runtime.Gameplay.Gate.Controller;
using Runtime.Gameplay.Gate.Model;
using Runtime.Gameplay.Gate.View;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Gate.Installer
{
    public class GateInstaller : MonoInstaller
    {
        [SerializeField]
        private GateModelConfig gateModelConfig;
        public override void InstallBindings()
        {
            Container.Bind<GateView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GateModel>().AsSingle().WithArguments(gateModelConfig);
            
            Container.BindInterfacesTo<GateValueTextUpdateController>().AsSingle();
            Container.BindInterfacesTo<GateCollisionController>().AsSingle();
        }
    }
}