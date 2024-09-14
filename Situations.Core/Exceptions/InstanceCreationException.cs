using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Situations.Core.Exceptions
{
    public class InstanceCreationException : Exception
    {
        public InstanceCreationException(Type type) : base($"Unable to create instance for type {type}") { }
        public InstanceCreationException(string message) : base(message) { }
        public InstanceCreationException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
