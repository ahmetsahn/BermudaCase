using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Gameplay.Gate.Model
{
    public class GateModel
    {
        public BuffType BuffType;

        public int BuffValue;

        public Color FeedBackColor;
        
        public GateModel(GateModelConfig config)
        {
            BuffType = config.BuffType;
            BuffValue = config.BuffValue;
            FeedBackColor = config.FeedBackColor;
        }
    }
    
    [Serializable]
    public struct GateModelConfig
    {
        public BuffType BuffType;
        
        public int BuffValue;

        public Color FeedBackColor;
    }
}