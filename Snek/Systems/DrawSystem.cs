
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Snek.Components;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 0)]
    class DrawSystem : EntityComponentProcessingSystem<Texture2DComponent, Vector2Component>
    {
        /// <summary>The content manager.</summary>
        private ContentManager contentManager;

        /// <summary>The sprite batch.</summary>
        private SpriteBatch spriteBatch;

        /// <summary>Override to implement code that gets executed when systems are initialized.</summary>
        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
        }

        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity, Texture2DComponent texture2DComponent, Vector2Component vector2Component)
        {
            spriteBatch.Draw(texture2DComponent.Texture2D, vector2Component.Vector2, Color.White);
        }
    }
}