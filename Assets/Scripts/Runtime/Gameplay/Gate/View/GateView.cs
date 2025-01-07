using System;
using Runtime.Core.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Gate.View
{
    public class GateView : MonoBehaviour, IInteractable
    {
        public BoxCollider BoxCollider;
        public Action OnInteract { get; set; }
    }
}