using Runtime.Signals;
using UnityEngine;

namespace Runtime.Gameplay.Buff
{
    public class LengthBuff : BaseBuff
    {
        public override void ApplyBuff(int buffValue)
        {
            SignalBus.Fire(new LengthBuffSignal(buffValue));
        }
    }
}