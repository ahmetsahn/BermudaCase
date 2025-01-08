using System;

namespace Runtime.Core.Interface
{
    public interface IKeyboardKey
    {
        public Action OnFeedBack { get; set; }
    }
}