using System;
using Runtime.Gameplay.Gate.Model;
using Runtime.Gameplay.Gate.View;

namespace Runtime.Gameplay.Gate.Controller
{
    public class GateCollisionController : IDisposable
    {
        private readonly GateView _view;
        
        private readonly GateModel _model;
        
        public GateCollisionController(GateView view, GateModel model)
        {
            _view = view;
            _model = model;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnZoneEnter += OnZoneEnter;
        }
        
        private void OnZoneEnter()
        {
            _view.BoxCollider.enabled = false;
            _model.BaseBuff.ApplyBuff(_model.BuffValue);
        }
        
        private void UnsubscribeEvents()
        {
            _view.OnZoneEnter -= OnZoneEnter;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}