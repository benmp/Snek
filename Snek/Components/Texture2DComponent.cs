using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snek.Components
{
    class Texture2DComponent : Component
    {
        const int _iD = 1;
        public override int ID => _iD;

        public Texture2D Texture2D;
        public Texture2DComponent(ContentManager contentManager, string ContentPath)
        {
            Texture2D = contentManager.Load<Texture2D>(ContentPath);
        }
    }
}
