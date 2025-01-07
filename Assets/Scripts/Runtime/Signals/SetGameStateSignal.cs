using Runtime.Core.Enums;

namespace Runtime.Signals
{
    public readonly struct SetGameStateSignal
    {
        public readonly GameState GameState;
        
        public SetGameStateSignal(GameState gameState)
        {
            GameState = gameState;
        }
    }
}