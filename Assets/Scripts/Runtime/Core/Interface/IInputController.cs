using UnityEngine;

namespace Runtime.Core.Interface
{
    public interface IInputController
    {
        Vector2 GetSwipeDelta(); 
        
        bool TapToStart();
    }
}