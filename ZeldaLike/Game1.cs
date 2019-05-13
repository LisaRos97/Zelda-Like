using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ZeldaLike
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Hero tash;
		Plant plant;
        List<Projectile> projectiles = new List<Projectile>();
		 List<Plant> plants = new List<Plant>();

        float cooldownCounter = 0;
       
        const float COOLDOWN = 0.2f;
        const float PLANTGROW = 0.2f;


        Tileset tileset;
        Tilemap tilemap;

        

        int[][] tilemapData = new int[][]
        {
             new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
             new int[] { 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
         };

        //Plant plant;

        Animation animation;


        int direction;     

     
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            tash = new ZeldaLike.Hero(100, 200, "hero");

            plant = new ZeldaLike.Plant(200, 100, "plant");

             animation = new ZeldaLike.Animation("MarcheDroite");
           //  animation.X = 300;
          //  animation.Y = 300;

            tileset = new Tileset(1, 3, 40, "tuiles");
            tilemap = new Tilemap(tileset, tilemapData);     

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tilemap.Load(Content);
            tash.Load(Content);
            plant.Load(Content);
            animation.Load(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)


        {
          


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            cooldownCounter += dt;
            tash.Update(gameTime, tilemap);
            plant.Update(gameTime);
            animation.Update(gameTime);



            // Priojectiles
            // ======================================================
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Space) && cooldownCounter > COOLDOWN)
            {
                Projectile projectile = new Projectile(tash);
                projectile.Load(Content);
                projectile.Visible = true;
                projectiles.Add(projectile);
                cooldownCounter = 0;


            }

            foreach (Projectile p in projectiles)
            {
                p.Update(gameTime);
            }

			if (ks.IsKeyDown(Keys.P)&& cooldownCounter > COOLDOWN)
			{
				Plant plant = new Plant(tash);
				plant.Load(Content);
				plant.Visible = true;
				plants.Add(plant);
				cooldownCounter = 0;


			}

			foreach (Plant c in plants)
			{
				c.Update(gameTime);
			}

            // intersections avec les plantes
            // ==============================================================

            direction = tash.Direction;

            if (tash.Rect.Intersects(plant.Rect))
            {
                if (direction == 6)
                    {
                    tash.X = tash.X -3;
                        }
                if (direction == 2)
                {
                    tash.Y = tash.Y - 3;
                }
                if (direction == 4)
                {
                    tash.X = tash.X + 3;
                }
                if (direction == 8)
                {
                  // if (tash.Y + 64 <= plant.Y && tash.Y + 64 >= plant.Y + 40)
                   // {
                        tash.Y = tash.Y + 3;
                  //  }
                }
            }
            //===============================================================

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            spriteBatch.Begin();
            tilemap.Draw(gameTime, spriteBatch);
            // plant.Draw(gameTime, spriteBatch);
            tash.Draw(gameTime, spriteBatch);
          //  animation.Draw(gameTime, spriteBatch);

            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

			 foreach (Plant c in plants)
			{
				c.Draw(gameTime, spriteBatch);
			}

           

            spriteBatch.End();

            base.Draw(gameTime);

        }
    
            
        }
    }


