namespace Runtime.Signals
{
    public readonly struct LengthBuffSignal
    {
        public readonly int BuffValue;
        
        public LengthBuffSignal(int buffValue)
        {
            BuffValue = buffValue;
        }
    }
}