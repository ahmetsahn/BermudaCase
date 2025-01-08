using System;
using System.Collections.Generic;
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
        public float ChildHandMoveDuration;
        
        public int CurrentWidth;
        public int CurrentLength;
        public int MaxWidth;
        public int MaxLength;
        
        public GameObject HandPrefab;
        
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
            ChildHandMoveDuration = config.ChildHandMoveDuration;
            CurrentWidth = config.DefaultWidth;
            CurrentLength = config.DefaultLength;
            MaxWidth = config.MaxWidth;
            MaxLength = config.MaxLength;
            HandPrefab = config.HandPrefab;
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
        public float ChildHandMoveDuration;
        
        public int MaxWidth;
        public int MaxLength;
        public int DefaultWidth;
        public int DefaultLength;
        
        public GameObject HandPrefab;
    }
}