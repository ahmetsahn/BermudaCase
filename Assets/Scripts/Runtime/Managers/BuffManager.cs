using System;
using AYellowpaper.SerializedCollections;
using Runtime.Enums;
using Runtime.Gameplay.Buff;
using Runtime.Signals;
using Zenject;

namespace Runtime.Managers
{
    public class BuffManager : IDisposable
    {
        private readonly SerializedDictionary<BuffType, BaseBuff> _buffDictionary;

        private readonly SignalBus _signalBus;

        public BuffManager(SignalBus signalBus, BuffManagerConfig config)
        {
            _signalBus = signalBus;
            _buffDictionary = config.BuffDictionary;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<ApplyBuffSignal>(OnApplyBuff);
        }

        private void OnApplyBuff(ApplyBuffSignal signal)
        {
            _buffDictionary[signal.BuffType].ApplyBuff(signal.BuffValue);
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<ApplyBuffSignal>(OnApplyBuff);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }

    [Serializable]
    public struct BuffManagerConfig
    {
        public SerializedDictionary<BuffType, BaseBuff> BuffDictionary;
    }
}
