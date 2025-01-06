using DG.Tweening;
using UnityEngine;

namespace Runtime.Gameplay.KeyboardKey.Model
{
    public class KeyboardKeyModel
    {
        public int TouchCount;
        
        public bool IsFirstTouch = true;
        
        public readonly Color FeedbackColor = Color.green;

        public const float FeedbackDuration = 0.1f;
        public const float FeedbackHeight = 0.6f;

        public const Ease FeedbackEase = Ease.InBounce;
    }
}