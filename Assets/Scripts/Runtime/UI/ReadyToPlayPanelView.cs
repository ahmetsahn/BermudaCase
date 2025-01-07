using Runtime.Enums;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.UI
{
    public class ReadyToPlayPanelView : MonoBehaviour
    {
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
            _signalBus.Fire(new CloseUIPanelSignal(UIPanelType.ReadyToPlayPanel));
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