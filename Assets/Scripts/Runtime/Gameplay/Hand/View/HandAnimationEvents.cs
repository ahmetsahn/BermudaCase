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
        
        public void EnableColliders()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                
                for (int j = 0; j < child.childCount; j++)
                {
                    Transform grandChild = child.GetChild(j);
                    
                    BoxCollider collider = grandChild.GetComponent<BoxCollider>();
                    if (collider != null)
                    {
                        collider.enabled = true;
                    }
                }
            }
        }
        
        public void PlayButtonClickSound()
        {
            _signalBus.Fire(new PlayAudioClipSignal(AudioClipType.ButtonClick));
        }
    }
}