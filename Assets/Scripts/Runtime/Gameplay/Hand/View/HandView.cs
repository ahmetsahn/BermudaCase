using System;
using Runtime.Core.Interface;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Hand.View
{
    public class HandView : MonoBehaviour
    {
        public BoxCollider BoxCollider;
        public event Action<Collider> OnTriggerCollider;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IKeyboardKey keyboardKey))
            {
                OnTriggerCollider?.Invoke(other);
                keyboardKey.OnFeedBack?.Invoke();
            }
        }
    }
}