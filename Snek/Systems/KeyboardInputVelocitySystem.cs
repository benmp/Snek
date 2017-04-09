
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework.Input;

using Snek.Components;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 1)]
    class KeyboardInputVelocitySystem : EntityComponentProcessingSystem<KeyboardInputComponent, VelocityComponent, SnakeHeadComponent>
    {
        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity, KeyboardInputComponent keyboardInputComponent, VelocityComponent velocityComponent, SnakeHeadComponent snakeHeadComponent)
        {
            if (keyboardInputComponent.NewKeyboardState.IsKeyDown(Keys.Right))
            {
                if (velocityComponent.Next.Angle != 180f)
                {
                    velocityComponent.Next.Angle = 0f;
                }
            }
            else if (keyboardInputComponent.NewKeyboardState.IsKeyDown(Keys.Up))
            {
                if (velocityComponent.Next.Angle != 90f)
                {
                    velocityComponent.Next.Angle = 270f;
                }
            }
            else if (keyboardInputComponent.NewKeyboardState.IsKeyDown(Keys.Left))
            {
                if (velocityComponent.Next.Angle != 0f)
                {
                    velocityComponent.Next.Angle = 180f;
                }
            }
            else if (keyboardInputComponent.NewKeyboardState.IsKeyDown(Keys.Down))
            {
                if (velocityComponent.Next.Angle != 270f)
                {
                    velocityComponent.Next.Angle = 90f;
                }
            }
        }
    }
}