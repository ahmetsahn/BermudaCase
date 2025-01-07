using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Buff
{
    public abstract class BaseBuff : MonoBehaviour
    {
        public abstract void ApplyBuff(int buffValue);
        
        protected SignalBus SignalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }
    }
}