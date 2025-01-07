using System;
using DG.Tweening;
using Runtime.Core.Interface;
using TMPro;
using UnityEngine;

namespace Runtime.Gameplay.KeyboardKey.View
{
    public class InteractableView : MonoBehaviour, IInteractable
    {
        public MeshRenderer MeshRenderer;
        
        public TextMeshPro TouchCountText;
        public Action OnInteract { get; set; }
    }
}