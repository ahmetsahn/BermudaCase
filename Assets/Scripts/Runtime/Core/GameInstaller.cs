using Runtime.Managers;
using Runtime.Signals;
using Zenject;

namespace Runtime.Core
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindSignals();
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        }
        
        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStartedSignal>();
            Container.DeclareSignal<SetGameStateSignal>();
        }
    }
}