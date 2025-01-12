using System;
using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.InputSystem
{
    public class TouchInputController : IInputController
    {
        private Vector2 _startTouchPosition;
        private Vector2 _swipeDelta;

        public Vector2 GetSwipeDelta()
        {
            if (Input.touchCount <= 0)
            {
                return _swipeDelta;
            }
            
            Touch touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    _swipeDelta = touch.position - _startTouchPosition;
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _swipeDelta = Vector2.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return _swipeDelta;
        }

        public bool TapToStart()
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }
}