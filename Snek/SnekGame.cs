
using Artemis;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snek.Components;
using System;

namespace Snek
{
    public class SnekGame : Game
    {
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);
        private readonly GraphicsDeviceManager graphics;
        private TimeSpan drawElapsedTime;
        private TimeSpan updateElapsedTime;
        private SpriteFont font;
        private int frameCounter;
        private int frameRate;
        private int updateCounter;
        private int updateRate;
        private SpriteBatch spriteBatch;
        private EntityWorld entityWorld;
        private Random random;

        DateTime currentTime;
        DateTime newTime;
        long deltaTime;
        long accumulator;
        long interval;

        public const byte TARGET_UPS = 120;

        public SnekGame()
        {
            this.drawElapsedTime = TimeSpan.Zero;
            this.updateElapsedTime = TimeSpan.Zero;

            this.graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferHeight = 720,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = false,
                PreferredDepthStencilFormat = DepthFormat.None
            };
#if DEBUG
            this.graphics.SynchronizeWithVerticalRetrace = false;
#else
            this.graphics.SynchronizeWithVerticalRetrace = true;
#endif
            this.IsFixedTimeStep = false; //this wont work when vsync is ON
            this.Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            this.random = new Random();

            currentTime = DateTime.Now;
            newTime = currentTime;
            accumulator = 0;
            interval = TimeSpan.TicksPerSecond / TARGET_UPS;
        }

        /// <summary>Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.</summary>
        protected override void Initialize()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.font = this.Content.Load<SpriteFont>("Arial12");

            this.entityWorld = new EntityWorld();

            EntitySystem.BlackBoard.SetEntry("ContentManager", this.Content);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", this.GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("SpriteBatch", this.spriteBatch);
            EntitySystem.BlackBoard.SetEntry("SpriteFont", this.font);
            EntitySystem.BlackBoard.SetEntry("EnemyInterval", 500);
            EntitySystem.BlackBoard.SetEntry("Random", this.random);

#if XBOX
            this.entityWorld.InitializeAll( System.Reflection.Assembly.GetExecutingAssembly());
#else
            this.entityWorld.InitializeAll(true);
#endif

            this.InitializePlayer();
            //this.InitializeEnemyShips();

            base.Initialize();
        }

        /// <summary>Allows the game to run logic such as updating the entityWorld,
        /// checking for collisions, gathering input, and playing audio.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newTime = DateTime.Now;
            deltaTime = newTime.Ticks - currentTime.Ticks;
            if (deltaTime > TimeSpan.TicksPerSecond)
            { //avoid spiral of death force draw to occur at least once per second
                deltaTime = TimeSpan.TicksPerSecond;
            }

            currentTime = newTime;
            accumulator += deltaTime;

            while (accumulator >= interval)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
                {
                    this.Exit();
                }

                this.entityWorld.Update(interval);
                accumulator -= interval;

                ++this.updateCounter;
                this.updateElapsedTime += TimeSpan.FromTicks(interval);
                if (this.updateElapsedTime > OneSecond)
                {
                    this.updateElapsedTime -= OneSecond;
                    this.updateRate = this.updateCounter;
                    this.updateCounter = 0;
                }
            }

            ++this.frameCounter;
            this.drawElapsedTime += TimeSpan.FromTicks(deltaTime);
            if (this.drawElapsedTime > OneSecond)
            {
                this.drawElapsedTime -= OneSecond;
                this.frameRate = this.frameCounter;
                this.frameCounter = 0;
            }

            return;
        }

        /// <summary>This is called when the game should draw itself.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            string fps = string.Format("fps: {0}", this.frameRate);
            string ups = string.Format("ups: {0}", this.updateRate);
#if DEBUG
            string entityCount = string.Format("Active entities: {0}", this.entityWorld.EntityManager.ActiveEntities.Count);
            //string removedEntityCount = string.Format("Removed entities: {0}", this.entityWorld.EntityManager.TotalRemoved);
            //string totalEntityCount = string.Format("Total entities: {0}", this.entityWorld.EntityManager.TotalCreated);
#endif

            this.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();
            this.entityWorld.Draw();
#if DEBUG
            this.spriteBatch.DrawString(this.font, ups, new Vector2(32, 16), Color.Yellow);
            this.spriteBatch.DrawString(this.font, fps, new Vector2(32, 32), Color.Yellow);
            this.spriteBatch.DrawString(this.font, entityCount, new Vector2(32, 62), Color.Yellow);
            //this.spriteBatch.DrawString(this.font, removedEntityCount, new Vector2(32, 92), Color.Yellow);
            //this.spriteBatch.DrawString(this.font, totalEntityCount, new Vector2(32, 122), Color.Yellow);
#endif
            this.spriteBatch.End();
        }

        /// <summary>The initialize player ship.</summary>
        private void InitializePlayer()
        {
            Entity entity = this.entityWorld.CreateEntity();
            entity.Group = "SNEKPC";

            entity.AddComponentFromPool<Texture2DComponent>();
            entity.AddComponentFromPool<VelocityComponent>();
            entity.AddComponentFromPool<KeyboardInputComponent>();
            entity.AddComponentFromPool<SnakeHeadComponent>();
            entity.AddComponentFromPool<SnakeBodyComponent>();
            entity.AddComponentFromPool<SnakeTailComponent>();

            entity.GetComponent<Texture2DComponent>().Init(Content, "Images/whitesquare");
            Velocity velocity = new Velocity();
            velocity.Init(16f, 0);
            entity.GetComponent<VelocityComponent>().Init(velocity, new Velocity());
            entity.GetComponent<KeyboardInputComponent>().Init(new KeyboardState(), new KeyboardState());
            entity.GetComponent<SnakeBodyComponent>().Init(new Vector2(128, 128), Vector2.Zero, entity);
            entity.Tag = "SNEKPCHEAD";
        }
    }
}