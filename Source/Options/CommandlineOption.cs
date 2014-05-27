using System.Collections.Generic;

namespace AutoHeader.Options
{
    public interface IOption
    {
        void Execute();
    }

    public abstract class Option : IOption
    {
        public string Arg { get; set; }
        
        public abstract void Execute();
    }
}