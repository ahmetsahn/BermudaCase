using System;
using Runtime.Core.Interface;
using UnityEngine;

namespace Runtime.Gameplay.Hand.View
{
    public class HandParentView : MonoBehaviour
    {
        public Action<Vector2> OnSwipe;
    }
}