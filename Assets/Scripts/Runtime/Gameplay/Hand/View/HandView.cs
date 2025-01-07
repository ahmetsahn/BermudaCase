using System;
using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.Gameplay.Hand.View
{
    public class HandView : MonoBehaviour
    {
        public Action<Vector2> OnSwipe;
    }
}