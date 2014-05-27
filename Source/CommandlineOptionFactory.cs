using System.Collections.Generic;
using AutoHeader.Options;

namespace AutoHeader
{
    public interface ICommandlineOptionFactory
    {
        Option CreateCommandlineOption(string option);
    }

    public class CommandlineOptionFactory : ICommandlineOptionFactory
    {
        private static readonly IDictionary<string, Option> KnownOptions = new Dictionary<string, Option>
            {
                { "-a", new AddHeaderOption() },
                { "-r" , new RemoveHeaderOption() }
            };
 
        public Option CreateCommandlineOption(string option)
        {
            if (!KnownOptions.Keys.Contains(option))
            {
                throw new UnknownOptionException(string.Format("Unknown option: {0}", option));
            }
            return KnownOptions[option];
        }
    }
}