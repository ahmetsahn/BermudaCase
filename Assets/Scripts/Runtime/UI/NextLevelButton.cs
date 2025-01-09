using Runtime.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.UI
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        
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
            button.onClick.AddListener(OnButtonClicked);
        }
        
        private void OnButtonClicked()
        {
            _signalBus.Fire(new NextLevelSignal());
        }
        
        private void UnsubscribeEvents()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}