using Artemis;
using Microsoft.Xna.Framework;

namespace Snek.Components
{
    //Add this Attribute and extend ComponentPoolable if you want your Component to use Artemis Component Pool
    [Artemis.Attributes.ArtemisComponentPool(InitialSize = 5, IsResizable = true, ResizeSize = 20, IsSupportMultiThread = true)]
    class SnakeBodyComponent : ComponentPoolable
    {
        public Vector2 Past { get; set; }
        public Vector2 Next { get; set; }
        public Entity LeadingPart { get; set; }
        public void Init(Vector2 next, Vector2 past, Entity leadingPart)
        {
            Past = past;
            Next = next;
            LeadingPart = leadingPart;
        }
    }
}
