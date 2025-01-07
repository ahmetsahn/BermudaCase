using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.InputHandler
{
    public class TouchInputHandler : IInputHandler
    {
        private Vector2 startTouchPosition;
        private Vector2 swipeDelta;

        public Vector2 GetSwipeDelta()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    swipeDelta = touch.position - startTouchPosition;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    swipeDelta = Vector2.zero;
                }
            }
            return swipeDelta;
        }

        public bool IsInputActive()
        {
            return Input.touchCount > 0;
        }

        public bool TapToStart()
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }
}