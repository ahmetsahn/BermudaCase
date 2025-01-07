using System;
using Runtime.Gameplay.Buff;
using UnityEngine.Serialization;

namespace Runtime.Gameplay.Gate.Model
{
    public class GateModel
    {
        public BaseBuff BaseBuff;

        public int BuffValue;
        
        public GateModel(GateModelConfig config)
        {
            BaseBuff = config.BaseBuff;
            BuffValue = config.BuffValue;
        }
    }
    
    [Serializable]
    public struct GateModelConfig
    {
        public BaseBuff BaseBuff;
        
        public int BuffValue;
    }
}