using System;

namespace Runtime.Core.Interface
{
    public interface IInteractable
    {
        public Action OnInteract { get; set; }
    }
}