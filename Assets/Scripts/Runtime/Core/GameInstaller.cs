﻿using Ahmet.ObjectPool;
using Runtime.Core.Interface;
using Runtime.InputSystem;
using Runtime.Managers;
using Runtime.Signals;
using Runtime.UI;
using UnityEngine;
using Zenject;

namespace Runtime.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private GameManagerConfig gameManagerConfig;
        
        [SerializeField]
        private LevelLoaderConfig levelLoaderConfig;
        
        [SerializeField]
        private UIManagerConfig uiManagerConfig;

        [SerializeField] 
        private BuffManagerConfig buffManagerConfig;

        [SerializeField] 
        private SoundManagerConfig soundManagerConfig;
        
        public override void InstallBindings()
        {
            BindServices();
            BindSignals();
        }
        
        private void BindServices()
        {
            Container.Bind<ObjectPoolManager>().AsSingle();
                
            Container.BindInterfacesTo<LevelLoader>().AsSingle().WithArguments(levelLoaderConfig);
            Container.BindInterfacesTo<UIManager>().AsSingle().WithArguments(uiManagerConfig);
            Container.BindInterfacesTo<BuffManager>().AsSingle().WithArguments(buffManagerConfig);
            Container.BindInterfacesTo<SoundManager>().AsSingle().WithArguments(soundManagerConfig);
            Container.BindInterfacesTo<InputController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().WithArguments(gameManagerConfig);
            
#if UNITY_ANDROID || UNITY_IOS
            Container.Bind<IInputController>().To<TouchInputController>().AsSingle();
#else
            Container.Bind<IInputController>().To<MouseInputController>().AsSingle();
#endif
        }
        
        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<SetGameStateSignal>();
            Container.DeclareSignal<NextLevelSignal>();
            Container.DeclareSignal<LoadLevelSignal>();
            Container.DeclareSignal<DestroyCurrentLevelSignal>();
            Container.DeclareSignal<OpenUIPanelSignal>();
            Container.DeclareSignal<CloseUIPanelSignal>();
            Container.DeclareSignal<CloseAllUIPanelsSignal>();
            Container.DeclareSignal<PushRateBuffSignal>();
            Container.DeclareSignal<WidthBuffSignal>();
            Container.DeclareSignal<LengthBuffSignal>();
            Container.DeclareSignal<ApplyBuffSignal>();
            Container.DeclareSignal<PlayAudioClipSignal>();
            Container.DeclareSignal<SpawnObjectSignal>();
            Container.DeclareSignal<EnableHandCollidersSignal>();
            Container.DeclareSignal<SwipeSignal>();
        }
    }
}