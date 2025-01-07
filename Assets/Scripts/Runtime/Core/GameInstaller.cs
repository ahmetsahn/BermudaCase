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
        public override void InstallBindings()
        {
            BindServices();
            BindSignals();
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().WithArguments(gameManagerConfig);
            
            Container.BindInterfacesTo<LevelLoader>().AsSingle().WithArguments(levelLoaderConfig);
            Container.BindInterfacesTo<UIManager>().AsSingle().WithArguments(uiManagerConfig);
            
        }
        
        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<GameStartedSignal>();
            Container.DeclareSignal<SetGameStateSignal>();
            Container.DeclareSignal<CompleteLevelSignal>();
            Container.DeclareSignal<LoadLevelSignal>();
            Container.DeclareSignal<DestroyCurrentLevelSignal>();
            Container.DeclareSignal<OpenUIPanelSignal>();
            Container.DeclareSignal<CloseUIPanelSignal>();
            Container.DeclareSignal<CloseAllUIPanelsSignal>();
        }
    }
}