using System;

namespace Runtime.Core.Interface
{
    public interface IZone
    {
        public Action OnZoneEnter { get; set; }
    }
}