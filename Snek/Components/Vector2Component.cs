using Artemis;
using Microsoft.Xna.Framework;

namespace Snek.Components
{
    //Add this Attribute and extend ComponentPoolable if you want your Component to use Artemis Component Pool
    [Artemis.Attributes.ArtemisComponentPool(InitialSize = 5, IsResizable = true, ResizeSize = 20, IsSupportMultiThread = true)]
    class Vector2Component : ComponentPoolable
    {
        public Vector2 Vector2 { get; set; }
        public bool Init(float positionX, float positionY)
        {
            Vector2 = new Vector2(positionX, positionY);
            return Vector2 != null;
        }
    }
}
