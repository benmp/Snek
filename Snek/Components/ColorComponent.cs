using Artemis;
using Microsoft.Xna.Framework;

namespace Snek.Components
{
    //Add this Attribute and extend ComponentPoolable if you want your Component to use Artemis Component Pool
    [Artemis.Attributes.ArtemisComponentPool(InitialSize = 5, IsResizable = true, ResizeSize = 20, IsSupportMultiThread = true)]
    class ColorComponent : ComponentPoolable
    {
        public Color Color { get; set; }
        public bool Init(Color color)
        {
            Color = color;
            return true;
        }
    }
}
