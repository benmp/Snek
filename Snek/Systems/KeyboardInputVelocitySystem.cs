
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework.Input;

using Snek.Components;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
    class KeyboardInputVelocitySystem : EntityComponentProcessingSystem<KeyboardInputComponent, VelocityComponent>
    {
        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity, KeyboardInputComponent keyboardInputComponent, VelocityComponent velocityComponent)
        {
            if (keyboardInputComponent.NewKeyboardState.IsKeyDown(Keys.Right))
            {
                velocityComponent.Angle = 0f;
            }
            else if (keyboardInputComponent.NewKeyboardState.IsKeyDown(Keys.Up))
            {
                velocityComponent.Angle = 270f;
            }
            else if (keyboardInputComponent.NewKeyboardState.IsKeyDown(Keys.Left))
            {
                velocityComponent.Angle = 180f;
            }
            else if (keyboardInputComponent.NewKeyboardState.IsKeyDown(Keys.Down))
            {
                velocityComponent.Angle = 90f;
            }
        }
    }
}