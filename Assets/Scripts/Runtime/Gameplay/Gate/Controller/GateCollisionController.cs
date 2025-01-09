using System;
using Runtime.Enums;
using Runtime.Gameplay.Gate.Model;
using Runtime.Gameplay.Gate.View;
using Runtime.Signals;
using Zenject;

namespace Runtime.Gameplay.Gate.Controller
{
    public class GateCollisionController : IDisposable
    {
        private readonly GateView _view;
        
        private readonly GateModel _model;

        private readonly SignalBus _signalBus;
        
        public GateCollisionController(GateView view, GateModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnZoneEnter += OnZoneEnter;
        }
        
        private void OnZoneEnter()
        {
            PlayGateCollisionSound();
            DisableColliderOppositeGate();
            DisableCollider();
            ApplyFeedBackColor();
            ApplyBuffEffect();
        }

        private void PlayGateCollisionSound()
        {
            _signalBus.Fire(_model.BuffValue > 0
                ? new PlayAudioClipSignal(AudioClipType.GoodGate,true)
                : new PlayAudioClipSignal(AudioClipType.BadGate,true));
        }

        private void DisableColliderOppositeGate()
        {
            if (_view.OppositeGateCollider != null)
            {
                _view.OppositeGateCollider.enabled = false;
            }
        }

        private void DisableCollider()
        {
            _view.GateCollider.enabled = false;
        }

        private void ApplyFeedBackColor()
        {
            _view.GradientSprite.color = _model.FeedBackColor;
        }

        private void ApplyBuffEffect()
        {
            _signalBus.Fire(new ApplyBuffSignal(_model.BuffType,_model.BuffValue));
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