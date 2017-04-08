using Artemis;

namespace Snek.Components
{
    //Add this Attribute and extend ComponentPoolable if you want your Component to use Artemis Component Pool
    [Artemis.Attributes.ArtemisComponentPool(InitialSize = 5, IsResizable = true, ResizeSize = 20, IsSupportMultiThread = true)]
    class VelocityComponent : ComponentPoolable
    {
        public float Speed { get; set; }
        public float Angle { get; set; }
        public bool Init(float speed, float angle)
        {
            Speed = speed;
            Angle = angle;
            return true;
        }
    }
}
