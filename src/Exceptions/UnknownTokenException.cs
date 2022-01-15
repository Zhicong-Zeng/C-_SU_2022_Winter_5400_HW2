using System;

namespace RecDescent.Exceptions
{
    public class UnknownTokenException : Exception
    {
        public UnknownTokenException(string unknownToken) : base(unknownToken) { }
    }
}