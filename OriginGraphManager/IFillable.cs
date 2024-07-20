using System;

namespace OriginGraphManager
{
    public interface IFillable
    {
        event Action OnFilling;
    }
}
