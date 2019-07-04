using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaLike
{
	class SceneGameOver : Scene
	{
		Sprite image;

		public override void Load(ContentManager content, Game1 game)
		{
			this.game = game;
			image = new Sprite(100, 100, "tash");
			image.Load(content);
			image.Visible = true;
		}

		public override void Update(GameTime gameTime)
		{
			image.X += 1;


		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			image.Draw(gameTime, spriteBatch);
		}
	}
}