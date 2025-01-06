using System;
using DG.Tweening;
using Runtime.Core.Interface;
using TMPro;
using UnityEngine;

namespace Runtime.Gameplay.KeyboardKey.View
{
    public class KeyboardKeyView : MonoBehaviour, IKeyboardKey
    {
        public MeshRenderer MeshRenderer;
        
        public TextMeshPro TouchCountText;

        public Action OnFeedback { get; set; }
    }
}