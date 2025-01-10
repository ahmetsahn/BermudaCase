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
        
        private readonly int _maxLevel;
        private int _currentLevelIndex;
        
        public GameManager(GameManagerConfig config, SignalBus signalBus)
        {
            _maxLevel = config.MaxLevel;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }

        public void Initialize()
        {
            
            _signalBus.Fire(new LoadLevelSignal(_currentLevelIndex));
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<SetGameStateSignal>(OnSetGameState);
            _signalBus.Subscribe<NextLevelSignal>(OnNextLevel);
        }
        
        private void OnNextLevel(NextLevelSignal signal)
        {
            _signalBus.Fire(new DestroyCurrentLevelSignal());
            IncreaseCurrentLevelIndex();
            _signalBus.Fire(new LoadLevelSignal(_currentLevelIndex));
        }
        
        private void SetTimeScale(int timeScale)
        {
            Time.timeScale = timeScale;
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
                    _signalBus.Fire(new CloseAllUIPanelsSignal());
                    _signalBus.Fire(new OpenUIPanelSignal(UIPanelType.LoadingPanel));
                    break;
                case GameState.ReadyToStart:
                    SetTimeScale(0);
                    _signalBus.Fire(new CloseAllUIPanelsSignal());
                    _signalBus.Fire(new OpenUIPanelSignal(UIPanelType.ReadyToPlayPanel));
                    break;
                case GameState.Playing:
                    _signalBus.Fire(new CloseUIPanelSignal(UIPanelType.ReadyToPlayPanel));
                    SetTimeScale(1);
                    break;
                case GameState.Finished:
                    SetTimeScale(0);
                    _signalBus.Fire(new OpenUIPanelSignal(UIPanelType.FinishPanel));
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
            _signalBus.Unsubscribe<NextLevelSignal>(OnNextLevel);
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