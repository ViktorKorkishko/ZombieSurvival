using System;

namespace Core.Exceptions
{
    public class EnumNotSupportedException<T> : Exception where T : Enum
    {
        public override string Message => $"Enum [{Enum.GetType().Name}] is not supported!";
        
        private T Enum { get; }

        public EnumNotSupportedException(T @enum)
        {
            Enum = @enum;
        }
    }
}
