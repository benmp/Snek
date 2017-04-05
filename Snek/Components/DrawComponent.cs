using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snek.Components
{
    class DrawComponent : Component
    {
        const int _iD = 0;
        public override int ID => _iD;

        SpriteBatch SpriteBatch;

        public DrawComponent(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }
    }
}
