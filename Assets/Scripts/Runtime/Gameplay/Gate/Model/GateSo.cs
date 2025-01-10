using UnityEngine;

namespace Runtime.Gameplay.Gate.Model
{
    [CreateAssetMenu(fileName = "GateData", menuName = "Scriptable Object/GateData", order = 0)]
    public class GateSo : ScriptableObject
    {
        public Color FeedBackColor;
    }
}