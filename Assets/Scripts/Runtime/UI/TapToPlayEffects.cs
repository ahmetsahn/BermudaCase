using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class TapToPlayEffects : MonoBehaviour
    {
        [SerializeField]
        private Text tapToPlayText;
        
        [SerializeField]
        public float maxScale = 1.5f; 
        [SerializeField]
        public float duration = 0.8f;

        private Tween _animationTween;
        
        private void Start()
        {
            AnimateText();
        }

        private void AnimateText()
        {
            _animationTween = tapToPlayText.transform
                .DOScale(maxScale, duration)
                .SetLoops(-1, LoopType.Yoyo)  
                .SetUpdate(true);
        }

        private void OnDisable()
        {
            _animationTween?.Kill();
        }
    }
}