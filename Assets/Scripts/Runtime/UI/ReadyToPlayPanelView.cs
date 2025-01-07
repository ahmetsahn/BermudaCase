﻿using Runtime.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.UI
{
    public class ReadyToPlayPanelView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI readyToPlayText;
        
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
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        }
        
        private void OnGameStarted()
        {
            readyToPlayText.gameObject.SetActive(false);
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}