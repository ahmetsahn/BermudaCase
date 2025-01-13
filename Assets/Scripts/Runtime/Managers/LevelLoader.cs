using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Object = UnityEngine.Object;

namespace Runtime.Managers
{
    public class LevelLoader : IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private readonly IInstantiator _instantiator;
        
        private readonly AssetReferenceGameObject[] _levelPrefabs;

        private GameObject _currentLevelInstance;
        
        private AsyncOperationHandle<GameObject> _currentLevelHandle;

        public LevelLoader(SignalBus signalBus, IInstantiator instantiator, LevelLoaderConfig config)
        {
            _signalBus = signalBus;
            _instantiator = instantiator;
            _levelPrefabs = config.LevelPrefabs;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<LoadLevelSignal>(OnLoadLevel);
            _signalBus.Subscribe<DestroyCurrentLevelSignal>(OnDestroyCurrentLevel);
        }

        private void OnLoadLevel(LoadLevelSignal signal)
        {
            _signalBus.Fire(new SetGameStateSignal(GameState.Loading));
            _currentLevelHandle = Addressables.LoadAssetAsync<GameObject>(_levelPrefabs[signal.LevelIndex]);
            _currentLevelHandle.Completed += OnLevelLoadComplete;
        }

        private void OnLevelLoadComplete(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status != AsyncOperationStatus.Succeeded)
            {
                return;
            }
            
            _currentLevelInstance = _instantiator.InstantiatePrefab(obj.Result);
            _signalBus.Fire(new SetGameStateSignal(GameState.ReadyToStart));
        }

        private void OnDestroyCurrentLevel(DestroyCurrentLevelSignal signal)
        {
            Object.Destroy(_currentLevelInstance);
            Addressables.ReleaseInstance(_currentLevelHandle);
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<LoadLevelSignal>(OnLoadLevel);
            _signalBus.Unsubscribe<DestroyCurrentLevelSignal>(OnDestroyCurrentLevel);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }

    [Serializable]
    public class LevelLoaderConfig
    {
        public AssetReferenceGameObject[] LevelPrefabs;
    }
}
