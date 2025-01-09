using System;
using Runtime.Core.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.Gate.View
{
    public class GateView : MonoBehaviour, IZone
    {
        public BoxCollider GateCollider;
        
        public BoxCollider OppositeGateCollider;
        
        public SpriteRenderer GradientSprite;
        
        public TextMeshPro BuffValueText;
        
        public Action OnZoneEnter { get; set; }
    }
}