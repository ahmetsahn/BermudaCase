using UnityEngine;

namespace Runtime.Core.Interface
{
    public interface IInputHandler
    {
        Vector2 GetSwipeDelta(); 
        
        bool IsInputActive();
        
        bool TapToStart();
    }
}