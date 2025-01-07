using System;
using UnityEngine;

namespace Runtime.Gameplay.Hand.Model
{
    public class HandModel
    {
        public float ColliderToggleDelay;
        public float PushRateIncreaseAmount;
        
        public float CurrentPushRate;
        public float CurrentWidth;
        public float CurrentLength;
        
        public string AnimationSpeedParameter = "PushRate";
        
        public HandModel(HandModelConfig config)
        {
            ColliderToggleDelay = config.ColliderToggleDelay;
            PushRateIncreaseAmount = config.PushRateIncreaseAmount;
            CurrentPushRate = config.DefaultPushRate;
            CurrentWidth = config.DefaultWidth;
            CurrentLength = config.DefaultLength;
        }
    }
    
    [Serializable]
    public struct HandModelConfig
    {
        public float ColliderToggleDelay;
        public float PushRateIncreaseAmount;
        public float DefaultPushRate;
        public float DefaultWidth;
        public float DefaultLength;
    }
}