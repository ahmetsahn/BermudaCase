namespace Runtime.Signals
{
    public readonly struct SpawnObjectSignal
    {
        public readonly bool IsForZenject;

        public SpawnObjectSignal(bool isForZenject)
        {
            IsForZenject = isForZenject;
        }
    }
}