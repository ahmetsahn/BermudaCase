using Runtime.Enums;

namespace Runtime.Signals
{
    public readonly struct PlayAudioClipSignal
    {
        public readonly AudioClipType AudioClipType;

        public PlayAudioClipSignal(AudioClipType audioClipType)
        {
            AudioClipType = audioClipType;
        }
    }
}