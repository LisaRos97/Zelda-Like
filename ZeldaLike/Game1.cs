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
		public List<Plant> plants = new List<Plant>();

		Scene currentScene;
		SceneGameOver gameover;
		SceneMap jeu;

		Tileset tileset;
		Tilemap tilemap;


		float cooldownCounter = 0;
       
        const float COOLDOWN = 0.2f;
        const float PLANTGROW = 0.2f;


		bool GWasPushed = false;
        

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
            tash = new Hero(100, 200, "tash", plants);

            plant = new Plant(200, 100, "plant");

            tileset = new Tileset(1, 3, 40, "tuiles");
            tilemap = new Tilemap(tileset, tilemapData);     

			gameover = new SceneGameOver();
			jeu = new SceneMap(tilemapData, "tuiles", (a) => ChangeScene(a) );
			currentScene = jeu;

            base.Initialize();
        }


		  public void SceneToGameOver()
		{
			gameover.Load(Content, this);
			currentScene = gameover;
		}

		void ChangeScene(Scene scene)
		{
			if (scene != null)
			{
				scene.Load(Content, this);
				currentScene = scene;
			}
		}

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
          spriteBatch = new SpriteBatch(GraphicsDevice);

			currentScene.Load(Content, this);

			// Create a new SpriteBatch, which can be used to draw textures.
           // spriteBatch = new SpriteBatch(GraphicsDevice);

           // tilemap.Load(Content);
           // tash.Load(Content);
            // plant.Load(Content);
          //  animation.Load(Content);

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


			if (Keyboard.GetState().IsKeyDown(Keys.G))
			{
				if (!GWasPushed)
				{
					if (currentScene == gameover)
					{
						currentScene = jeu;
					}

					else
					{
						SceneToGameOver();
					}
				}
				GWasPushed = true;
			}
			else
			{
				GWasPushed = false;
			}

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			currentScene.Update(gameTime);

			base.Update(gameTime);

		}

            // intersections avec les plantes
            // ==============================================================

           // direction = tash.Direction;

          //  if (tash.Rect.Intersects(plant.Rect))
         //   {
               // if (direction == 6)
                  //  {
                  //  tash.X = tash.X -3;
                       // }
               // if (direction == 2)
              //  {
               //     tash.Y = tash.Y - 3;
                //}
                //if (direction == 4)
               // {
                   // tash.X = tash.X + 3;
               // }
                //if (direction == 8)
                //{
                  // if (tash.Y + 64 <= plant.Y && tash.Y + 64 >= plant.Y + 40)
                   // {
                    //    tash.Y = tash.Y + 3;
                  //  }
               // }
            //}
            //===============================================================

       // }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin();

			currentScene.Draw(gameTime, spriteBatch);

			spriteBatch.End();
			base.Draw(gameTime);

        }
    
            
        }
    }


