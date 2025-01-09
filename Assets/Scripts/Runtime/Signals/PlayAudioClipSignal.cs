using Runtime.Enums;

namespace Runtime.Signals
{
    public readonly struct PlayAudioClipSignal
    {
        public readonly AudioClipType AudioClipType;

        public readonly bool IsEffect;

        public PlayAudioClipSignal(AudioClipType audioClipType, bool isEffect = false)
        {
            AudioClipType = audioClipType;
            IsEffect = isEffect;
        }
    }
}