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
        public float PushRateIncreaseAmount;
        public float MinPushRateSpeed;
        public float MaxPushRateSpeed;
        public float CurrentPushRateSpeed;
        public float DistanceBetweenHands;
        public float ChildHandMoveDuration;
        
        public int MaxWidth;
        public int MaxLength;
        public int MinWidth;
        public int MinLength;
        public int CurrentWidth;
        public int CurrentLength;
        
        public GameObject HandPrefab;
        
        public string PushAnimationSpeedParameter = "PushRateSpeed";
        
        public HandParentModel(HandParentModelConfig config)
        {
            MinX = config.MinX;
            MaxX = config.MaxX;
            HorizontalSpeed = config.HorizontalSpeed;
            ForwardSpeed = config.ForwardSpeed;
            PushRateIncreaseAmount = config.PushRateIncreaseAmount;
            MinPushRateSpeed = config.MinPushRateSpeed;
            MaxPushRateSpeed = config.MaxPushRateSpeed;
            CurrentPushRateSpeed = config.MinPushRateSpeed;
            DistanceBetweenHands = config.DistanceBetweenHands;
            ChildHandMoveDuration = config.ChildHandMoveDuration;
            MaxWidth = config.MaxWidth;
            MaxLength = config.MaxLength;
            MinWidth = config.MinWidth;
            MinLength = config.MinLength;
            CurrentWidth = config.MinWidth;
            CurrentLength = config.MinLength;
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
        public float PushRateIncreaseAmount;
        public float MinPushRateSpeed;
        public float MaxPushRateSpeed;
        public float DistanceBetweenHands;
        public float ChildHandMoveDuration;
        
        public int MaxWidth;
        public int MaxLength;
        public int MinWidth;
        public int MinLength;
        
        public GameObject HandPrefab;
    }
}