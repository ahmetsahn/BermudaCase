using Runtime.Signals;

namespace Runtime.Gameplay.Buff
{
    public class WidthBuff : BaseBuff
    {
        public override void ApplyBuff(int buffValue)
        {
            SignalBus.Fire(new WidthBuffSignal(buffValue));
        }
    }
}