using System;
using UnityEngine;

namespace Runtime.Gameplay.Hand.Model
{
    public class HandModel
    {
        public float MinX;
        public float MaxX;
        public float HorizontalSpeed;
        public float ForwardSpeed;
        
        public Vector2 SwipeDelta;
        
        public HandModel(HandModelConfig config)
        {
            MinX = config.MinX;
            MaxX = config.MaxX;
            HorizontalSpeed = config.HorizontalSpeed;
            ForwardSpeed = config.ForwardSpeed;
        }
    }
    
    [Serializable]
    public struct HandModelConfig
    {
        public float MinX;
        public float MaxX;  
        public float HorizontalSpeed;
        public float ForwardSpeed;
    }
}