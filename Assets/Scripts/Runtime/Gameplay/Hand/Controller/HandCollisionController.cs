using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.Gameplay.Hand.Controller
{
    public class HandCollisionController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IKeyboardKey keyboardKey))
            {
                keyboardKey.OnFeedback?.Invoke();
            }
        }
    }
}