using Runtime.Gameplay.Hand.Controller;
using Runtime.Gameplay.Hand.View;
using Zenject;

namespace Runtime.Gameplay.Hand.Installer
{
    public class HandInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HandView>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesTo<HandCollisionController>().AsSingle();
        }
    }
}