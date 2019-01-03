using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindergarten
{
    class ChildInfoController
    {
        public Child Child { get; set; }

        public ChildInfoController(Child child)
        {
            Child = child;
        }
    }
}
