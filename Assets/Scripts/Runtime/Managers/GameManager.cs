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

        private int _currentLevelIndex;
        private int _maxLevel;
        
        public GameManager(GameManagerConfig config, SignalBus signalBus)
        {
            _maxLevel = config.MaxLevel;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }

        public void Initialize()
        {
            _signalBus.Fire(new OpenUIPanelSignal(UIPanelType.ReadyToPlayPanel));
            LoadCurrentLevel();
            _signalBus.Fire(new SetGameStateSignal(GameState.ReadyToStart));
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<SetGameStateSignal>(OnSetGameState);
            _signalBus.Subscribe<CompleteLevelSignal>(OnCompleteLevel);
        }
        
        private void LoadCurrentLevel()
        {
            _signalBus.Fire(new LoadLevelSignal(_currentLevelIndex));
        }
        
        private void OnCompleteLevel(CompleteLevelSignal signal)
        {
            _signalBus.Fire(new DestroyCurrentLevelSignal());
            IncreaseCurrentLevelIndex();
            _signalBus.Fire(new CloseUIPanelSignal(UIPanelType.FinishPanel));
            _signalBus.Fire(new OpenUIPanelSignal(UIPanelType.ReadyToPlayPanel));
            LoadCurrentLevel();
            _signalBus.Fire(new SetGameStateSignal(GameState.ReadyToStart));
        }
        
        private void IncreaseCurrentLevelIndex()
        {
            _currentLevelIndex++;
            if (_currentLevelIndex >= _maxLevel)
            {
                _currentLevelIndex = 0;
            }
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
            _signalBus.Unsubscribe<CompleteLevelSignal>(OnCompleteLevel);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
    
    [Serializable]
    public class GameManagerConfig
    {
        public int MaxLevel;
    }
}