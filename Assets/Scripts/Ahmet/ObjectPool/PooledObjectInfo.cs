using System.Collections.Generic;
using UnityEngine;

namespace Script.Ahmet.ObjectPool
{
    public class PooledObjectInfo
    {
        public string LookupString;
        public readonly List<GameObject> InactiveObjects = new();
    }
}