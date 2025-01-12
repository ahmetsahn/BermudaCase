using Runtime.Core.Interface;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using Zenject;

namespace Runtime.InputSystem
{
    public class InputController : ITickable
    {
        private readonly IInputController _inputController;
        
        private readonly SignalBus _signalBus;
        
        private readonly GameManager _gameManager;
        
        public InputController(IInputController inputController, SignalBus signalBus, GameManager gameManager)
        {
            _inputController = inputController;
            _signalBus = signalBus;
            _gameManager = gameManager;
        }
        
        public void Tick()
        {
            if (_inputController.TapToStart() && _gameManager.GetGameState().Equals(GameState.ReadyToStart))
            {
                _signalBus.Fire(new SetGameStateSignal(GameState.Playing));
            }

            if (!_gameManager.GetGameState().Equals(GameState.Playing))
            {
                return;
            }
            
            _signalBus.Fire(new SwipeSignal(_inputController.GetSwipeDelta()));
        }
    }
}