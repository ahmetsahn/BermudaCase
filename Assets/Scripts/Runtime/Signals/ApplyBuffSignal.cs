using Runtime.Enums;

namespace Runtime.Signals
{
    public readonly struct ApplyBuffSignal
    {
        public readonly BuffType BuffType;

        public readonly int BuffValue;

        public ApplyBuffSignal(BuffType buffType, int buffValue)
        {
            BuffType = buffType;
            BuffValue = buffValue;
        }
    }
}