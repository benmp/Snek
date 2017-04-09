
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;

using Snek.Components;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 2)]
    class SnakeBodyMovementSystem : EntityComponentProcessingSystem<VelocityComponent, SnakeBodyComponent>
    {
        private int updateCounter = 0;

        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            ++updateCounter;
            if (updateCounter == SnekGame.TARGET_UPS / 12) //TODO 2 UPS
            {
                Debug.WriteLine(TimeSpan.FromTicks(this.EntityWorld.Delta).TotalMilliseconds);
                foreach (Entity entity in entities.Values.OrderBy(v => v.Id))
                {
                    Process(entity, entity.GetComponent<VelocityComponent>(), entity.GetComponent<SnakeBodyComponent>());
                }

                updateCounter = 0;
            }
            return;
        }

        public override void Process(Entity entity, VelocityComponent velocityComponent, SnakeBodyComponent snakeBodyComponent)
        {
            double ms = TimeSpan.FromTicks(this.EntityWorld.Delta).TotalMilliseconds;
            snakeBodyComponent.Past = snakeBodyComponent.Next;
            velocityComponent.Previous = velocityComponent.Next;
            snakeBodyComponent.Next = new Vector2(snakeBodyComponent.Past.X + (float)(Math.Cos(velocityComponent.Previous.Angle * (Math.PI / 180.0)) * velocityComponent.Previous.Speed), //TODO put this value in a component based off texture height
                                                  snakeBodyComponent.Past.Y + (float)(Math.Sin(velocityComponent.Previous.Angle * (Math.PI / 180.0)) * velocityComponent.Previous.Speed));

            Velocity VCLead = snakeBodyComponent.LeadingPart.GetComponent<VelocityComponent>().Previous;
            Velocity vc = new Velocity();
            vc.Init(VCLead.Speed, VCLead.Angle);
            velocityComponent.Next = vc;
        }
    }
}