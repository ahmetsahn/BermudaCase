using System;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentPushRateBuff : IDisposable
    {
        private readonly HandParentView _view;
        
        private readonly HandParentModel _model;
        
        private readonly SignalBus _signalBus;

        public HandParentPushRateBuff(HandParentView view, HandParentModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _signalBus.Subscribe<PushRateBuffSignal>(OnPushRateBuffSignal);
        }

        private void UnsubscribeFromEvents()
        {
            _signalBus.Unsubscribe<PushRateBuffSignal>(OnPushRateBuffSignal);
        }

        private void OnPushRateBuffSignal(PushRateBuffSignal signal)
        {
            float newSpeed = CalculateNewPushRateSpeed(signal.BuffValue);
            _view.Animator.SetFloat(_model.PushAnimationSpeedParameter, newSpeed);
            _model.CurrentPushRateSpeed = newSpeed;
        }

        private float CalculateNewPushRateSpeed(float buffValue)
        {
            float currentSpeed = _model.CurrentPushRateSpeed;
            float updatedSpeed = currentSpeed + _model.PushRateIncreaseAmount * buffValue;
            
            AnimationClip clip = _view.Animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            float maxSpeed = clip.length / _model.MaxPushRateSpeed;
            
            updatedSpeed = Mathf.Clamp(updatedSpeed, _model.MinPushRateSpeed, maxSpeed);

            return updatedSpeed;
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}