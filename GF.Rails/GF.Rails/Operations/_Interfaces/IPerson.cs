using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Operations
{
    public interface IPerson
    {
        public PersonId Id { get; }

        public string DisplayName { get; }
    }
}
