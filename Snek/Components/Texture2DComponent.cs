using Artemis;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Snek.Components
{
    //Add this Attribute and extend ComponentPoolable if you want your Component to use Artemis Component Pool
    [Artemis.Attributes.ArtemisComponentPool(InitialSize = 5, IsResizable = true, ResizeSize = 20, IsSupportMultiThread = true)]
    class Texture2DComponent : ComponentPoolable
    {
        public Texture2D Texture2D { get; set; }
        public bool Init(ContentManager contentManager, string ContentPath)
        {
            Texture2D = contentManager.Load<Texture2D>(ContentPath);
            return Texture2D != null;
        }
    }
}
