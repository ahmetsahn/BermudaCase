using System;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Gameplay.Zones;
using UnityEngine;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandCollisionController : IDisposable
    {
        private readonly HandView _view;

        private readonly HandModel _model;
        public HandCollisionController(HandView view, HandModel model)
        {
            _view = view;
            _model = model;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnTriggerCollider += OnTriggerCollider;
        }
        
        private void OnTriggerCollider(Collider collider)
        {
            if (!collider.TryGetComponent(out FinishZone finishZone))
            {
                ToggleColliderTemporarily().Forget();
            }
        }
        
        private async UniTaskVoid ToggleColliderTemporarily()
        {
            _view.BoxCollider.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_model.ColliderToggleDelay));
            _view.BoxCollider.enabled = true;
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnTriggerCollider -= OnTriggerCollider;
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}