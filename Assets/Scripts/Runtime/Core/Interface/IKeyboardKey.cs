using System;

namespace Runtime.Core.Interface
{
    public interface IKeyboardKey
    {
        public Action OnFeedback { get; set; }
    }
}