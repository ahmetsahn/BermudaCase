using Runtime.Gameplay.KeyboardKey.Controller;
using Runtime.Gameplay.KeyboardKey.Model;
using Runtime.Gameplay.KeyboardKey.View;
using Zenject;

namespace Runtime.Gameplay.KeyboardKey.Installer
{
    public class KeyboardKeyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<KeyboardKeyView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<KeyboardKeyModel>().AsSingle();
            
            Container.BindInterfacesTo<KeyboardKeyTouchController>().AsSingle();
        }
    }
}