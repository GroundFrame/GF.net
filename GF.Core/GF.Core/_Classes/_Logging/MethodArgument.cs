using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Core
{
    internal class MethodArgument<T>
    {
        internal string Name { get; private set; }

        internal T? Value { get; private set; }

        internal MethodArgument(string name, T value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
