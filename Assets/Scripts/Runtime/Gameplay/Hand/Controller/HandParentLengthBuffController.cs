using System;
using Runtime.Gameplay.Hand.Model;
using Runtime.Gameplay.Hand.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandParentLengthBuffController : IDisposable
    {
        private readonly HandParentView _view;
        
        private readonly HandParentModel _model;
        
        private readonly SignalBus _signalBus;
        
        public HandParentLengthBuffController(HandParentView view, HandParentModel model, SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<LengthBuffSignal>(OnLengthBuff);
        }
        
        private void OnLengthBuff()
        {
            Debug.Log("Length buff applied");
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<LengthBuffSignal>(OnLengthBuff);
        }
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}