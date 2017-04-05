using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snek.Systems
{
    abstract class System
    {
        public bool UseUpdateInterval = true;
        public abstract void Update(float dt);

        public abstract void Init();

        public List<int> RequiredComponentIDs = new List<int>();
    }
}
