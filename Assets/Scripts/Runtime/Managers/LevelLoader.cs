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

        public LevelLoader(SignalBus signalBus, IInstantiator instantiator, LevelLoaderConfig config)
        {
            _signalBus = signalBus;
            _instantiator = instantiator;
            _levelPrefabs = config.LevelPrefabs;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<LoadLevelSignal>(OnLoadLevelAsync);
            _signalBus.Subscribe<DestroyCurrentLevelSignal>(OnDestroyCurrentLevel);
        }

        private async void OnLoadLevelAsync(LoadLevelSignal signal)
        {
            try
            {
                if (_currentLevelInstance != null)
                {
                    DestroyCurrentLevel();
                }

                _signalBus.Fire(new SetGameStateSignal(GameState.Loading));
                GameObject levelPrefab = await Addressables.LoadAssetAsync<GameObject>(_levelPrefabs[signal.LevelIndex]).Task;
                _currentLevelInstance = _instantiator.InstantiatePrefab(levelPrefab);
                _signalBus.Fire(new SetGameStateSignal(GameState.ReadyToStart));
            }
            
            catch (Exception ex)
            {
                Debug.LogError($"Level loading failed: {ex.Message}");
            }
        }

        private void OnDestroyCurrentLevel(DestroyCurrentLevelSignal signal)
        {
            DestroyCurrentLevel();
        }

        private void DestroyCurrentLevel()
        {
            if (_currentLevelInstance != null)
            {
                Object.Destroy(_currentLevelInstance);
                _currentLevelInstance = null;
            }
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<LoadLevelSignal>(OnLoadLevelAsync);
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
