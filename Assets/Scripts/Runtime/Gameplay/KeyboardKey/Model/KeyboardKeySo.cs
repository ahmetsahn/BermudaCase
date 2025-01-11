using DG.Tweening;
using UnityEngine;

namespace Runtime.Gameplay.KeyboardKey.Model
{
    [CreateAssetMenu(fileName = "KeyboardKeyData", menuName = "Scriptable Object/KeyboardKeyData")]
    public class KeyboardKeySo : ScriptableObject
    {
        public float FeedbackDuration;
        public float FeedbackHeight;
        
        public Color FeedbackColor;

        public Ease FeedbackEase;
    }
}