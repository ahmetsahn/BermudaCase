using System.Collections.Generic;
using System.Linq;
using Script.Ahmet.ObjectPool;
using UnityEngine;
using Zenject;

namespace Ahmet.ObjectPool
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private static readonly List<PooledObjectInfo> ObjectPools = new();
        
        private static IInstantiator _instantiator;

        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);
            
            if (pool == null)
            {
                pool = new PooledObjectInfo { LookupString = objectToSpawn.name };
                ObjectPools.Add(pool);
            }

            GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();
            
            if (spawnableObject == null)
            {
                spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            }
            else
            {
                pool.InactiveObjects.Remove(spawnableObject);
                spawnableObject.transform.position = spawnPosition;
                spawnableObject.transform.rotation = spawnRotation;
                spawnableObject.SetActive(true);
            }
            
            return spawnableObject;
        }
        
        public static GameObject SpawnObjectForZenject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);
            
            if (pool == null)
            {
                pool = new PooledObjectInfo { LookupString = objectToSpawn.name };
                ObjectPools.Add(pool);
            }

            GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();
            
            if (spawnableObject == null)
            {
                spawnableObject = _instantiator.InstantiatePrefab(objectToSpawn, spawnPosition, spawnRotation, null);
            }
            else
            {
                pool.InactiveObjects.Remove(spawnableObject);
                spawnableObject.transform.position = spawnPosition;
                spawnableObject.transform.rotation = spawnRotation;
                spawnableObject.SetActive(true);
            }
            
            return spawnableObject;
        }
        
        public static GameObject SpawnObjectForZenject(GameObject objectToSpawn, Transform parent)
        {
            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);
            
            if (pool == null)
            {
                pool = new PooledObjectInfo { LookupString = objectToSpawn.name };
                ObjectPools.Add(pool);
            }

            GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();
            
            if (spawnableObject == null)
            {
                spawnableObject = _instantiator.InstantiatePrefab(objectToSpawn, parent);
            }
            else
            {
                pool.InactiveObjects.Remove(spawnableObject);
                spawnableObject.transform.SetParent(parent);
                spawnableObject.transform.localPosition = Vector3.zero;
                spawnableObject.SetActive(true);
            }
            
            return spawnableObject;
        }

        public static void ReturnObjectToPool(GameObject objectToReturn)
        {
            string goName = objectToReturn.name.Substring(0, objectToReturn.name.Length - 7);
            
            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);
            
            if (pool == null)
            {
                Debug.LogError("Pool not found for object: " + goName);
            }
            
            objectToReturn.SetActive(false);
            pool.InactiveObjects.Add(objectToReturn);
        }

        private void OnDisable()
        {
            ObjectPools.Clear();
        }
    }
}
