using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Gameplay.KeyboardKey.Model
{
    public class KeyboardKeyModel
    {
        public KeyboardKeySo Data;
        
        public int TouchCount;
        
        public bool IsFirstTouch = true;

        public KeyboardKeyModel(KeyboardKeyModelConfig config)
        {
            Data = config.Data;
        }
    }

    [Serializable]
    public struct KeyboardKeyModelConfig
    {
        public KeyboardKeySo Data;
    }
}