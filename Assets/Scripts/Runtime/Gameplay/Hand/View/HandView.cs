using System;
using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.Gameplay.Hand.View
{
    public class HandView : MonoBehaviour
    {
        public BoxCollider BoxCollider;
        public event Action OnTriggerCollider;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IKeyboardKey keyboardKey))
            {
                OnTriggerCollider?.Invoke();
                keyboardKey.OnFeedBack?.Invoke();
            }
        }
    }
}