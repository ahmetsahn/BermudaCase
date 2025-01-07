using System;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandMovementController : ITickable, IDisposable
    {
        private readonly HandView _view;
        
        private readonly HandModel _model;

        private float _currentX;
        private float _currentZ;

        public HandMovementController(HandView view, HandModel model)
        {
            _view = view;
            _model = model;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _view.OnSwipe += OnSwipe;
        }
        
        public void Tick()
        {
            ForwardMove();
        }

        private void OnSwipe(Vector2 direction)
        {
            _model.SwipeDelta = direction;
            
            HorizontalMove();
        }

        private void HorizontalMove()
        {
            _currentX += _model.SwipeDelta.x * _model.HorizontalSpeed * Time.deltaTime;
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
            _view.OnSwipe -= OnSwipe;
        }
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}