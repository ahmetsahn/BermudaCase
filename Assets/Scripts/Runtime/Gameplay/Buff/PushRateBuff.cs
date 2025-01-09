using Runtime.Signals;

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