using System;
using AYellowpaper.SerializedCollections;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Managers
{
    public class SoundManager : IDisposable
    {
        private readonly AudioSource _audioSource;
        
        private readonly SerializedDictionary<AudioClipType, AudioClip> _audioClipsDictionary;

        private readonly SignalBus _signalBus;
        
        public SoundManager(SoundManagerConfig config, SignalBus signalBus)
        {
            _audioSource = config.AudioSource;
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
            _audioSource.PlayOneShot(_audioClipsDictionary[signal.AudioClipType]);
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
        public AudioSource AudioSource;
        
        public SerializedDictionary<AudioClipType, AudioClip> AudioClipsDictionary;
    }
}