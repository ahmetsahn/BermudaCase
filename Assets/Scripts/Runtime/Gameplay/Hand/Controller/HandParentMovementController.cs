using System;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentMovementController : ITickable, IDisposable
    {
        private readonly HandParentView _parentView;
        
        private readonly HandParentModel _parentModel;
        
        private float _currentX;
        private float _currentZ;

        public HandParentMovementController(HandParentView parentView, HandParentModel parentModel)
        {
            _parentView = parentView;
            _parentModel = parentModel;
            
            Initialize();
            SubscribeEvents();
        }
        
        private void Initialize()
        {
            _currentX = _parentView.transform.position.x;
            _currentZ = _parentView.transform.position.z;
        }

        private void SubscribeEvents()
        {
            _parentView.OnSwipe += OnSwipe;
        }
        
        public void Tick()
        {
            ForwardMove();
        }

        private void OnSwipe(Vector2 direction)
        {
            _parentModel.SwipeDelta = direction;
            
            HorizontalMove();
        }

        private void HorizontalMove()
        {
            _currentX += _parentModel.SwipeDelta.x * _parentModel.HorizontalSpeed * Time.deltaTime;
            _currentX = Mathf.Clamp(_currentX, _parentModel.MinX, _parentModel.MaxX);
            UpdateTransform();
        }

        private void ForwardMove()
        {
            _currentZ += _parentModel.ForwardSpeed * Time.deltaTime;
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            _parentView.transform.position = new Vector3(_currentX, _parentView.transform.position.y, _currentZ);
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