using System;
using Runtime.Core.Interface;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.View
{
    public class HandView : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        
        public BoxCollider BoxCollider;
        public event Action OnTriggerCollider;
        
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnInteract?.Invoke();
                OnTriggerCollider?.Invoke();
            }
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
            animator.enabled = true;
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