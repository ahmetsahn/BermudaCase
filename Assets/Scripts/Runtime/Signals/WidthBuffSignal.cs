namespace Runtime.Signals
{
    public readonly struct WidthBuffSignal
    {
        public readonly int BuffValue;
        
        public WidthBuffSignal(int buffValue)
        {
            BuffValue = buffValue;
        }
    }
}