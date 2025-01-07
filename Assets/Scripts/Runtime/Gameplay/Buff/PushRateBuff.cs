using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Buff
{
    public class PushRateBuff : BaseBuff
    {
        public override void ApplyBuff(int buffValue)
        {
            SignalBus.Fire(new PushRateBuffSignal(buffValue));
        }
    }
}