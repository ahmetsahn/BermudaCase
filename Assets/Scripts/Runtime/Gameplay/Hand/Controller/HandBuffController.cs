using System;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandBuffController : IDisposable
    {
        private readonly HandView _view;
        
        private readonly HandModel _model;
        
        private readonly SignalBus _signalBus;
        
        public HandBuffController(HandView view, HandModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<PushRateBuffSignal>(OnPushRateBuff);
            _signalBus.Subscribe<WidthBuffSignal>(OnWidthBuff);
            _signalBus.Subscribe<LengthBuffSignal>(OnLengthBuff);
        }

        private void OnPushRateBuff(PushRateBuffSignal signal)
        {
            float currentSpeed = _view.Animator.GetFloat(_model.AnimationSpeedParameter);
            Debug.Log("CurrentSpeed :" + currentSpeed);
            float newSpeed = currentSpeed + _model.PushRateIncreaseAmount * signal.BuffValue;
            _view.Animator.SetFloat(_model.AnimationSpeedParameter, newSpeed);
            Debug.Log("NewSpeed: "+ _view.Animator.GetFloat(_model.AnimationSpeedParameter));
        }
        
        private void OnWidthBuff(WidthBuffSignal signal)
        {
            Debug.Log("Width buff applied");
        }
        
        private void OnLengthBuff(LengthBuffSignal signal)
        {
            Debug.Log("Length buff applied");
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<PushRateBuffSignal>(OnPushRateBuff);
            _signalBus.Unsubscribe<WidthBuffSignal>(OnWidthBuff);
            _signalBus.Unsubscribe<LengthBuffSignal>(OnLengthBuff);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}