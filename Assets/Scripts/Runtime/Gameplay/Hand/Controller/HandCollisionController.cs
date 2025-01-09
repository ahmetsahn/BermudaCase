﻿using System;
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

        private readonly SignalBus _signalBus;

        public HandCollisionController(HandView view, SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _view.OnTriggerCollider += OnTriggerCollider;
        }
        
        private void OnTriggerCollider()
        {
            DisableCollider();
        }
        
        private void DisableCollider()
        {
            _view.BoxCollider.enabled = false;
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