using System;
using DG.Tweening;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentBuffController : IDisposable
    {
        private readonly HandParentView _view;
        
        private readonly HandParentModel _model;
        
        private readonly SignalBus _signalBus;

        private readonly IInstantiator _instantiator;
        
        public HandParentBuffController(HandParentView view, HandParentModel model, SignalBus signalBus, IInstantiator instantiator)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            _instantiator = instantiator;
            
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
            Debug.Log("Push rate buff applied");
        }
        
        private void OnWidthBuff(WidthBuffSignal signal)
        {
            for (int i = 0; i < signal.BuffValue; i++)
            {
                _model.Hands[i].SetActive(true);
            }
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