using System;

namespace Core.SaveSystem.Entity
{
    public class GuidFactory
    {
        public Guid GetGuid()
        {
            var guid = Guid.NewGuid();
            return guid;
        }
    }
}