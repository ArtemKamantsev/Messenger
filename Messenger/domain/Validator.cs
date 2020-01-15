using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class NameValidator
    {
        public static NameValidator Instance { get; private set; } = new NameValidator();
        private NameValidator() { }

        public bool isValid(String name)
        {
            return name.CompareTo("") != 0;
        }
    }
}
