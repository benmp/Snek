
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Artemis.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Snek.Components;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 4)]
    class EatDotSystem : ProcessingSystem
    {
        private ContentManager contentManager;

        private GraphicsDevice graphicsDevice;

        public override void LoadContent()
        {
            graphicsDevice = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
            base.LoadContent();
        }

        public override void ProcessSystem()
        {
            Entity dotEntity = EntityWorld.TagManager.GetEntity("SNEKDOT");
            if (dotEntity == null)
            {
                return;
            }

            Bag<Entity> snakeBodies  = EntityWorld.EntityManager.GetEntities(Aspect.All(typeof(SnakeBodyComponent)));

            foreach (Entity snakeBodyEntity in snakeBodies)
            {
                if (Vector2.Distance(dotEntity.GetComponent<DotComponent>().Vector2, snakeBodyEntity.GetComponent<SnakeBodyComponent>().Next) == 0)
                {
                    dotEntity.AddComponentFromPool<EatenComponent>();

                    Entity tailEntity = EntityWorld.EntityManager.GetEntities(Aspect.All(typeof(SnakeTailComponent)))[0];

                    Entity entity = this.entityWorld.CreateEntity();
                    entity.Group = "SNEKBODY";

                    entity.AddComponentFromPool<Texture2DComponent>();
                    entity.AddComponentFromPool<VelocityComponent>();
                    entity.AddComponentFromPool<SnakeBodyComponent>();
                    entity.AddComponentFromPool<SnakeTailComponent>();

                    entity.GetComponent<Texture2DComponent>().Init(contentManager, "Images/whitesquare");
                    Velocity velocity = new Velocity();
                    velocity.Init(16f, tailEntity.GetComponent<VelocityComponent>().Previous.Angle);
                    entity.GetComponent<VelocityComponent>().Init(velocity, new Velocity());
                    entity.GetComponent<SnakeBodyComponent>().Init(tailEntity.GetComponent<SnakeBodyComponent>().Past, Vector2.Zero, tailEntity);

                    tailEntity.RemoveComponent<SnakeTailComponent>();
                    break;
                }
            }
        }
    }
}