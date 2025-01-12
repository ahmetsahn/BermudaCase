using System;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentMovementController : ITickable, IDisposable
    {
        private readonly HandParentView _view;
        
        private readonly HandParentModel _model;

        private readonly SignalBus _signalBus;
        
        private float _currentX;
        private float _currentZ;

        public HandParentMovementController(HandParentView view, HandParentModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            
            Initialize();
            SubscribeEvents();
        }
        
        private void Initialize()
        {
            _currentX = _view.transform.position.x;
            _currentZ = _view.transform.position.z;
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<SwipeSignal>(OnSwipe);
        }
        
        public void Tick()
        {
            ForwardMove();
        }

        private void OnSwipe(SwipeSignal signal)
        {
            HorizontalMove(signal.Direction);
        }

        private void HorizontalMove(Vector2 direction)
        {
            _currentX += direction.x * _model.HorizontalSpeed * Time.deltaTime;
            _currentX = Mathf.Clamp(_currentX, _model.MinX, _model.MaxX);
            UpdateTransform();
        }

        private void ForwardMove()
        {
            _currentZ += _model.ForwardSpeed * Time.deltaTime;
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            _view.transform.position = new Vector3(_currentX, _view.transform.position.y, _currentZ);
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<SwipeSignal>(OnSwipe);
        }
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}