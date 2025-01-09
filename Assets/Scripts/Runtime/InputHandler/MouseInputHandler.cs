using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.InputHandler
{
    public class MouseInputHandler : IInputHandler
    {
        private Vector2 _startMousePosition;
        private Vector2 _swipeDelta;

        public Vector2 GetSwipeDelta()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startMousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _swipeDelta = Vector2.zero;
            }
            return _swipeDelta;
        }

        public bool IsInputActive()
        {
            return Input.GetMouseButton(0);
        }

        public bool TapToStart()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}