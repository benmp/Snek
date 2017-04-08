
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework.Input;
using Snek.Components;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 0)]
    class KeyboardInputSystem : EntityComponentProcessingSystem<KeyboardInputComponent>
    {
        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity, KeyboardInputComponent keyboardInputComponent)
        {
            keyboardInputComponent.OldKeyboardState = keyboardInputComponent.NewKeyboardState;
            keyboardInputComponent.NewKeyboardState = Keyboard.GetState();
        }
    }
}