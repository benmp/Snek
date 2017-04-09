
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Snek.Components;
using System;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 5)]
    class DotPositionSystem : EntityComponentProcessingSystem<DotComponent>
    {
        private ContentManager contentManager;

        private GraphicsDevice graphicsDevice;

        public override void LoadContent()
        {
            graphicsDevice = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
            if (this.entityWorld.TagManager.GetEntity("SNEKDOT") == null)
            {
                CreateDotEntity();
            }
            base.LoadContent();
        }
        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity, DotComponent dotComponent)
        {
            EatenComponent eatenComponent = entity.GetComponent<EatenComponent>();
            if (eatenComponent != null)
            {
                ResetDotEntityPosition(entity);
                entity.RemoveComponent<EatenComponent>();
            }
        }

        public void CreateDotEntity()
        {
            Entity entity = this.entityWorld.CreateEntity();

            entity.Group = "SNEKDOTS";

            entity.AddComponentFromPool<Texture2DComponent>();
            entity.AddComponentFromPool<ColorComponent>();
            entity.AddComponentFromPool<DotComponent>();

            entity.GetComponent<Texture2DComponent>().Init(contentManager, "Images/whitesquare");
            entity.GetComponent<ColorComponent>().Init(Color.Blue);
            ResetDotEntityPosition(entity);
            entity.Tag = "SNEKDOT";
        }

        public void ResetDotEntityPosition(Entity entity)
        {
            Random random = BlackBoard.GetEntry<Random>("Random");
            int x = random.Next(graphicsDevice.Viewport.Width / 16);
            int y = random.Next(graphicsDevice.Viewport.Height / 16);
            entity.GetComponent<DotComponent>().Init(x * 16, y * 16);
        }
    }
}