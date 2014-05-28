using System;

namespace AutoHeader
{
    public class UnknownOptionException : Exception
    {
        public UnknownOptionException(string message) : base(message) {}
    }

    public class ExecutionException : Exception
    {
        public ExecutionException(string message) : base(message) {}
    }
}