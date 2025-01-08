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
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<PushRateBuffSignal>(OnPushRateBuffSignal);
        }
        
        private void OnPushRateBuffSignal(PushRateBuffSignal signal)
        {
            Debug.Log("PushRateBuffSignal");
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<PushRateBuffSignal>(OnPushRateBuffSignal);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}