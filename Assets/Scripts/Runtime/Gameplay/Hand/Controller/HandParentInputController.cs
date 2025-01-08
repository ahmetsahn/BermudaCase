using System;
using Runtime.Core.Interface;
using Runtime.Enums;
using Runtime.Gameplay.Hand.View;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentInputController : ITickable, IDisposable
    {
        private readonly HandParentView _parentView;
        
        private readonly IInputHandler _inputHandler;
        
        private readonly SignalBus _signalBus;
        
        private readonly GameManager _gameManager;
        
        public HandParentInputController(HandParentView parentView, IInputHandler inputHandler, SignalBus signalBus, GameManager gameManager)
        {
            _parentView = parentView;
            _inputHandler = inputHandler;
            _signalBus = signalBus;
            _gameManager = gameManager;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _parentView.OnSwipe += OnSwipe;
        }
        
        private void OnSwipe(Vector2 direction)
        {
            if (_inputHandler.IsInputActive())
            {
                return;
            }
            
            _inputHandler.GetSwipeDelta();
        }
        
        public void Tick()
        {
            if (_inputHandler.TapToStart() && _gameManager.GetGameState().Equals(GameState.ReadyToStart))
            {
                _signalBus.Fire(new SetGameStateSignal(GameState.Playing));
            }
            
            if (_inputHandler.IsInputActive())
            {
                _parentView.OnSwipe?.Invoke(_inputHandler.GetSwipeDelta());
            }
        }

        private void UnsubscribeEvents()
        {
            _parentView.OnSwipe -= OnSwipe;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}