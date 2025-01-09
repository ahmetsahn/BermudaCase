using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class ReadyToPlayPanelView : MonoBehaviour
    {
        [SerializeField]
        private Text tapToPlayText;
        
        [SerializeField]
        private float maxScale = 1.5f; 
        [SerializeField]
        private float duration = 0.8f;

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