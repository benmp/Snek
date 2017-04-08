using Artemis;
using Microsoft.Xna.Framework.Input;

namespace Snek.Components
{
    //Add this Attribute and extend ComponentPoolable if you want your Component to use Artemis Component Pool
    [Artemis.Attributes.ArtemisComponentPool(InitialSize = 5, IsResizable = true, ResizeSize = 20, IsSupportMultiThread = true)]
    class KeyboardInputComponent : ComponentPoolable
    {
        public KeyboardState OldKeyboardState { get; set; }
        public KeyboardState NewKeyboardState { get; set; }
        public bool Init(KeyboardState oldKeyboardState, KeyboardState newKeyboardState)
        {
            OldKeyboardState = oldKeyboardState;
            NewKeyboardState = newKeyboardState;
            return true;
        }
    }
}
