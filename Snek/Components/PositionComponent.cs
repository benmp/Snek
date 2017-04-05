using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snek.Components
{
    class Vector2Component : Component
    {
        const int _iD = 1;
        public override int ID => _iD;

        public Vector2 Vector2;
        public Vector2Component(float positionX, float positionY)
        {
            Vector2 = new Vector2(positionX, positionY);
        }

    }
}
