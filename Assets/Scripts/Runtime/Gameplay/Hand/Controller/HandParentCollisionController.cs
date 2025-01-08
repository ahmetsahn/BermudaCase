using System;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentCollisionController : IDisposable
    {
        private readonly HandParentView _view;
        
        private readonly HandParentModel _model;
        
        public HandParentCollisionController(HandParentView view, HandParentModel model)
        {
            _view = view;
            _model = model;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnCollisionZone += OnCollisionZone;
        }
        
        private void OnCollisionZone()
        {
            ToggleColliderTemporarily().Forget();
        }
        
        private async UniTaskVoid ToggleColliderTemporarily()
        {
            _view.BoxCollider.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_model.ColliderToggleDelay));
            _view.BoxCollider.enabled = true;
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnCollisionZone -= OnCollisionZone;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}