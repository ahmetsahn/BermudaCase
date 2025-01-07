using System;
using Runtime.Core.Interface;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Zones
{
    public class FinishZone : MonoBehaviour, IInteractable
    {
        public Action OnInteract { get; set; }
        
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            OnInteract += OnInteractHandler;
        }

        private void OnInteractHandler()
        {
            Time.timeScale = 0;
            _signalBus.Fire(new OpenUIPanelSignal(UIPanelType.FinishPanel));
        }
        
        private void UnsubscribeEvents()
        {
            OnInteract -= OnInteractHandler;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}