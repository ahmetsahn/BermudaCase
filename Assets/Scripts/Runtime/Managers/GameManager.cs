using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Managers
{
    public class GameManager : IInitializable, IDisposable
    {
        private GameState _currentGameState;
        
        private readonly SignalBus _signalBus;
        
        public GameManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            SubscribeEvents();
        }

        public void Initialize()
        {
            _signalBus.Fire(new SetGameStateSignal(GameState.ReadyToStart));
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<SetGameStateSignal>(OnSetGameState);
        }

        private void OnSetGameState(SetGameStateSignal signal)
        {
            _currentGameState = signal.GameState;

            switch (_currentGameState)
            {
                case GameState.Loading:
                    break;
                case GameState.ReadyToStart:
                    break;
                case GameState.Playing:
                    _signalBus.Fire(new GameStartedSignal());
                    break;
                case GameState.GameOver:
                    break;
            }
        }

        public GameState GetGameState()
        {
            return _currentGameState;
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<SetGameStateSignal>(OnSetGameState);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}