using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.InputHandler
{
    public class MouseInputHandler : IInputHandler
    {
        private Vector2 startMousePosition;
        private Vector2 swipeDelta;

        public Vector2 GetSwipeDelta()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startMousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swipeDelta = Vector2.zero;
            }
            return swipeDelta;
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