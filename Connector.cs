using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    public class Connector
    {
        public int Out { get; set; }

        public override string ToString()
        {
            return $"Out:{Out}";
        }
    }
}