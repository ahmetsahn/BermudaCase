using System;
using AYellowpaper.SerializedCollections;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Runtime.Managers
{
    public class SoundManager : IDisposable
    {
        private readonly AudioSource _buttonAudioSource;
        
        private readonly AudioSource _effectAudioSource;
        
        private readonly SerializedDictionary<AudioClipType, AudioClip> _audioClipsDictionary;

        private readonly SignalBus _signalBus;
        
        public SoundManager(SoundManagerConfig config, SignalBus signalBus)
        {
            _buttonAudioSource = config.ButtonAudioSource;
            _effectAudioSource = config.EffectAudioSource;
            _audioClipsDictionary = config.AudioClipsDictionary;
            _signalBus = signalBus;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<PlayAudioClipSignal>(OnPlayAudioClip);
        }
        
        private void OnPlayAudioClip(PlayAudioClipSignal signal)
        {
            if (signal.IsEffect)
            {
                _effectAudioSource.PlayOneShot(_audioClipsDictionary[signal.AudioClipType]);
                return;
            }
            
          
            _buttonAudioSource.pitch = Random.Range(0.95f, 1.05f);
            _buttonAudioSource.PlayOneShot(_audioClipsDictionary[signal.AudioClipType]);
            
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<PlayAudioClipSignal>(OnPlayAudioClip);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
    
    [Serializable]
    public struct SoundManagerConfig
    {
        public AudioSource ButtonAudioSource;
        public AudioSource EffectAudioSource;
        
        public SerializedDictionary<AudioClipType, AudioClip> AudioClipsDictionary;
    }
}