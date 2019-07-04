using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike
{
	class SceneMap : Scene
	{
		Hero tash;
		List<Plant> plants = new List<Plant>();
		float cooldownCounter = 0;
		const float COOLDOWN = 0.1f;

		Tileset tileset;
	Tilemap tilemap;


		public SceneMap(int[][] tilemapData, string tilesetPath, ChangeSceneFunc changeScene)
		{
			tash= new Hero(100, 100, "tash", plants);
			tileset = new Tileset(1, 3, 40, tilesetPath);
			tilemap = new Tilemap(tileset, tilemapData);


			this.changeScene = changeScene;
		}

		public override void Load(ContentManager content, Game1 game)
		{
			this.game = game;
			this.content = content;

			tash.Load(content);
			tilemap.Load(content);

		}

		public override void Update(GameTime gameTime)
		{
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			cooldownCounter += dt;

			// TODO: Add your update logic here
			tash.Update(gameTime, tilemap);


			KeyboardState ks = Keyboard.GetState();
			if (ks.IsKeyDown(Keys.P) && cooldownCounter > COOLDOWN)
			{
				Plant plant = new Plant(tash);
				plant.Load(content);
				plant.Visible = true;
				plants.Add(plant);
				cooldownCounter = 0;


			}



					
				

			foreach (Plant c in plants)
			{
				c.Update(gameTime);
			}



		}


		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			tilemap.Draw(gameTime, spriteBatch);
			tash.Draw(gameTime, spriteBatch);
			foreach (Plant c in plants)
			{
				c.Draw(gameTime, spriteBatch);
			}
		}
	}
}