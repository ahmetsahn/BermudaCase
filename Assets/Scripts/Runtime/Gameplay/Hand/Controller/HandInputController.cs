using System;
using Runtime.Core.Interface;
using Runtime.Gameplay.Hand.View;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandInputController : ITickable, IDisposable
    {
        private readonly HandView _view;
        
        private readonly IInputHandler _inputHandler;
        
        public HandInputController(HandView view, IInputHandler inputHandler)
        {
            _view = view;
            _inputHandler = inputHandler;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnSwipe += OnSwipe;
        }
        
        private void OnSwipe(UnityEngine.Vector2 direction)
        {
            if (_inputHandler.IsInputActive())
            {
                return;
            }
            
            _inputHandler.GetSwipeDelta();
        }


        public void Tick()
        {
            if (_inputHandler.IsInputActive())
            {
                _view.OnSwipe?.Invoke(_inputHandler.GetSwipeDelta());
            }
        }

        private void UnsubscribeEvents()
        {
            _view.OnSwipe -= OnSwipe;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}