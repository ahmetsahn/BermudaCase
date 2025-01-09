using System;
using Cysharp.Threading.Tasks;
using Runtime.Enums;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandCollisionController : IDisposable
    {
        private readonly HandView _view;
        
        private readonly HandModel _model;

        private readonly SignalBus _signalBus;

        public HandCollisionController(HandView view, HandModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnTriggerCollider += OnTriggerCollider;
        }
        
        private void OnTriggerCollider()
        {
            PlayButtonClickSound();
            ToggleColliderTemporarily().Forget();
        }

        private void PlayButtonClickSound()
        {
            _signalBus.Fire(new PlayAudioClipSignal(AudioClipType.ButtonClick));
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