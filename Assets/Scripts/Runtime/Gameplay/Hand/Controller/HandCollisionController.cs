using System;
using Cysharp.Threading.Tasks;
using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandCollisionController : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider boxCollider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IKeyboardKey keyboardKey))
            {
                keyboardKey.OnFeedback?.Invoke();
                ToggleColliderTemporarily().Forget();
            }
        }
        
        private async UniTaskVoid ToggleColliderTemporarily()
        {
            boxCollider.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            boxCollider.enabled = true;
        }
    }
}