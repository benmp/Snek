
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;

using Snek.Components;
using System;
using System.Diagnostics;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 2)]
    class MovementSystem : EntityComponentProcessingSystem<VelocityComponent, Vector2Component>
    {
        private int updateCounter = 0;
        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity, VelocityComponent velocityComponent, Vector2Component vector2Component)
        {
            ++updateCounter;
            double ms = TimeSpan.FromTicks(this.EntityWorld.Delta).TotalMilliseconds;
            Debug.WriteLine(TimeSpan.FromTicks(this.EntityWorld.Delta).TotalMilliseconds);
            if (updateCounter == 120) //TODO 2 UPS
            {
                vector2Component.Vector2 = new Vector2(vector2Component.Vector2.X + (float)(Math.Cos(velocityComponent.Angle * (Math.PI / 180.0)) * velocityComponent.Speed), //TODO put this value in a component based off texture height
                                                       vector2Component.Vector2.Y + (float)(Math.Sin(velocityComponent.Angle * (Math.PI / 180.0)) * velocityComponent.Speed));
                updateCounter = 0;
            }
        }
    }
}