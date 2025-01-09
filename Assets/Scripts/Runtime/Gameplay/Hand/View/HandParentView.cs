using System;
using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.Gameplay.Hand.View
{
    public class HandParentView : MonoBehaviour
    {
        public Animator Animator;
        
        public Transform[] LineTransforms;
        
        public Action<Vector2> OnSwipe;
        
        public Action OnCollisionZone;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IZone zone))
            {
                zone.OnZoneEnter();
                OnCollisionZone?.Invoke();
            }
        }
    }
}