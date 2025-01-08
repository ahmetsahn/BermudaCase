using System;
using UnityEngine;

namespace Runtime.Gameplay.Hand.Model
{
    public class HandModel
    {
        public float ColliderToggleDelay;
        
        public HandModel(HandModelConfig config)
        {
            ColliderToggleDelay = config.ColliderToggleDelay;
        }
    }
    
    [Serializable]
    public struct HandModelConfig
    {
        public float ColliderToggleDelay;
    }
}