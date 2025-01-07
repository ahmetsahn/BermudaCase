namespace Runtime.Signals
{
    public readonly struct PushRateBuffSignal
    {
        public readonly int BuffValue;
        
        public PushRateBuffSignal(int buffValue)
        {
            BuffValue = buffValue;
        }
    }
}