
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Snek.Components;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 1)]
    class DotDrawSystem : EntityComponentProcessingSystem<Texture2DComponent, DotComponent>
    {
        /// <summary>The sprite batch.</summary>
        private SpriteBatch spriteBatch;

        /// <summary>Override to implement code that gets executed when systems are initialized.</summary>
        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
        }

        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity, Texture2DComponent texture2DComponent, DotComponent dotComponent)
        {
            spriteBatch.Draw(texture2DComponent.Texture2D, dotComponent.Vector2, Color.White);
        }
    }
}