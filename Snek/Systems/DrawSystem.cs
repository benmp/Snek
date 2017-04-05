using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snek.Systems
{
    class DrawSystem : System
    {
        public override void Init()
        {
            base.RequiredComponentIDs.Add(0);
            base.RequiredComponentIDs.Add(1);
            base.RequiredComponentIDs.Add(2);

            base.UseUpdateInterval = false;
        }

        public override void Update(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
