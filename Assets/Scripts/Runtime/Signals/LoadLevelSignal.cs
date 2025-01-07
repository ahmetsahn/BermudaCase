namespace Runtime.Signals
{
    public readonly struct LoadLevelSignal
    {
        public readonly int LevelIndex;
        
        public LoadLevelSignal(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}