using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Gameplay.Gate.Model
{
    public class GateModel
    {
        public GateSo Data;
        
        public BuffType BuffType;

        public int BuffValue;
        
        public GateModel(GateModelConfig config)
        {
            Data = config.Data;
            BuffType = config.BuffType;
            BuffValue = config.BuffValue;
        }
    }
    
    [Serializable]
    public struct GateModelConfig
    {
        public GateSo Data;
        
        public BuffType BuffType;
        
        public int BuffValue;
    }
}