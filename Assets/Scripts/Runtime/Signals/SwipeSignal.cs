using UnityEngine;

namespace Runtime.Signals
{
    public readonly struct SwipeSignal
    {
        public readonly Vector2 Direction;

        public SwipeSignal(Vector2 direction)
        {
            Direction = direction;
        }
    }
}