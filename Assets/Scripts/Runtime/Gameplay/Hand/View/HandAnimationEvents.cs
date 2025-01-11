using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.View
{
    public class HandAnimationEvents : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void EnableHandColliders()
        {
            _signalBus.Fire(new EnableHandCollidersSignal());
        }
        
        public void PlayButtonClickSound()
        {
            _signalBus.Fire(new PlayAudioClipSignal(AudioClipType.ButtonClick));
        }
    }
}