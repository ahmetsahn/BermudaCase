using Runtime.Gameplay.KeyboardKey.Controller;
using Runtime.Gameplay.KeyboardKey.Model;
using Runtime.Gameplay.KeyboardKey.View;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.KeyboardKey.Installer
{
    public class KeyboardKeyInstaller : MonoInstaller
    {
        [SerializeField] 
        private KeyboardKeyModelConfig keyboardKeyModelConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<KeyboardKeyView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<KeyboardKeyModel>().AsSingle().WithArguments(keyboardKeyModelConfig);
            
            Container.BindInterfacesTo<KeyboardKeyTouchController>().AsSingle();
        }
    }
}