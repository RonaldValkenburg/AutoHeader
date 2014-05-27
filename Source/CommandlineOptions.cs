using System.Collections;
using System.Collections.Generic;
using AutoHeader.Options;

namespace AutoHeader
{
    public class CommandlineOptions : IEnumerable<IOption>
    {
        private readonly IList<IOption> options = new List<IOption>();

        public CommandlineOptions(IEnumerable<string> args)
        {
            ParseOptions(args);
        }

        private void ParseOptions(IEnumerable<string> args)
        {
            var factory = new CommandlineOptionFactory();
            Option previous = null;
            foreach (var arg in args)
            {
                if (arg.StartsWith("-"))
                {
                    previous = factory.CreateCommandlineOption(arg);
                    options.Add(previous);
                }
                else if (previous != null)
                {
                    previous.Arg = arg;
                }
                else
                {
                    throw new UnknownOptionException(string.Format("Unknown option: {0}", arg));
                }
            }
        }

        public IEnumerator<IOption> GetEnumerator()
        {
            return options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}