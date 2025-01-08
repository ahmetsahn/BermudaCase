using System;
using UnityEngine;

namespace Runtime.Gameplay.Hand.Model
{
    public class HandParentModel
    {
        public float MinX;
        public float MaxX;
        public float HorizontalSpeed;
        public float ForwardSpeed;
        public float ColliderToggleDelay;
        public float PushRateIncreaseAmount;
        public float CurrentPushRateSpeed;
        public float MaxPushRateSpeed;
        public float DistanceBetweenHands;
        
        public int CurrentWidth;
        public int CurrentLength;
        public int MaxWidth;
        public int MaxLength;
        
        public GameObject[] Hands;
        
        public string PushAnimationSpeedParameter = "PushRateSpeed";
        
        public Vector2 SwipeDelta;
        
        public HandParentModel(HandParentModelConfig config)
        {
            MinX = config.MinX;
            MaxX = config.MaxX;
            HorizontalSpeed = config.HorizontalSpeed;
            ForwardSpeed = config.ForwardSpeed;
            ColliderToggleDelay = config.ColliderToggleDelay;
            PushRateIncreaseAmount = config.PushRateIncreaseAmount;
            CurrentPushRateSpeed = config.DefaultPushRateSpeed;
            MaxPushRateSpeed = config.MaxPushRateSpeed;
            DistanceBetweenHands = config.DistanceBetweenHands;
            CurrentWidth = config.DefaultWidth;
            CurrentLength = config.DefaultLength;
            MaxWidth = config.MaxWidth;
            MaxLength = config.MaxLength;
            Hands = config.Hands;
        }
    }
    
    [Serializable]
    public struct HandParentModelConfig
    {
        public float MinX;
        public float MaxX;  
        public float HorizontalSpeed;
        public float ForwardSpeed;
        public float ColliderToggleDelay;
        public float PushRateIncreaseAmount;
        public float DefaultPushRateSpeed;
        public float MaxPushRateSpeed;
        public float DistanceBetweenHands;
        
        public int MaxWidth;
        public int MaxLength;
        public int DefaultWidth;
        public int DefaultLength;
        
        public GameObject[] Hands;
    }
}