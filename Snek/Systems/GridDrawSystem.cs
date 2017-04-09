using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Snek.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 0)]
    class GridDrawSystem : ProcessingSystem
    {
        /// <summary>The sprite batch.</summary>
        private SpriteBatch spriteBatch;

        private Texture2D texture2D;

        private GraphicsDevice graphicsDevice;

        /// <summary>Override to implement code that gets executed when systems are initialized.</summary>
        public override void LoadContent()
        {
            this.spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            ContentManager contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
            texture2D = contentManager.Load<Texture2D>("Images/whitepixel");

            this.graphicsDevice = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
        }

        /// <summary>Processes the specified entity.</summary>
        public override void ProcessSystem()
        {
            for (int h = 0; h < graphicsDevice.Viewport.Height; h += 16)
            {
                spriteBatch.Draw(texture2D, new Rectangle(0, h, graphicsDevice.Viewport.Width, 1), Color.Gray);
            }
            for (int w = 0; w < graphicsDevice.Viewport.Width; w += 16)
            {
                spriteBatch.Draw(texture2D, new Rectangle(w, 0, 1, graphicsDevice.Viewport.Height), Color.Gray);
            }
        }
    }
}