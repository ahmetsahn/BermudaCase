﻿using System;
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
            _signalBus.Subscribe<LoadLevelSignal>(OnLoadLevel);
            _signalBus.Subscribe<DestroyCurrentLevelSignal>(OnDestroyCurrentLevel);
        }

        private async void OnLoadLevel(LoadLevelSignal signal)
        {
            try
            {
                _signalBus.Fire(new SetGameStateSignal(GameState.Loading));
                
                var levelPrefabHandle = await Addressables.LoadAssetAsync<GameObject>(_levelPrefabs[signal.LevelIndex]).Task;
                
                _currentLevelInstance = _instantiator.InstantiatePrefab(levelPrefabHandle);
                
                _signalBus.Fire(new SetGameStateSignal(GameState.ReadyToStart));
            }
            catch (Exception ex)
            {
                Debug.LogError($"Level loading failed: {ex.Message}");
            }
        }
        
        private void OnDestroyCurrentLevel(DestroyCurrentLevelSignal signal)
        {
            if (_currentLevelInstance != null)
            {
                Object.Destroy(_currentLevelInstance);
                Addressables.ReleaseInstance(_currentLevelInstance);
            }
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