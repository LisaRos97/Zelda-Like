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
	public class Hero : Sprite
	{

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public int Direction { get; set; }

		public bool isMovingRight { get; set; }
		public bool isMoving { get; set; }

		public int Speed = 2;
		Texture2D image1;
		Texture2D image2;
		Texture2D image3;
		Texture2D image4;
		Animation marcheDroite;
		Animation marche;
		Animation marcheGauche;
		Animation marcheDos;
		List<Plant> plants; 






		public Hero tash;
		Plant plant;


		private object content;


		public Rectangle Rect
		{
			get
			{
				return new Rectangle((int)(X + Ox), (int)(Y + Oy + 50), image.Width, image.Height - 50);
			}

		}






		public Hero(int x, int y, string path, List<Plant>plants) : base(x, y, path)
		{
			Visible = true;
			Direction = 6;
			marcheDroite = new Animation("AnimationMarcheDroite");

			Visible = true;
			Direction = 2;
			marche = new Animation("AnimationMarche");

			Visible = true;
			Direction = 4;
			marcheGauche = new Animation("AnimationMarcheGauche");

			Visible = true;
			Direction = 8;
			marcheDos = new Animation("AnimationMarcheDos");

			this.plants = plants;
		}

		public override void Load(ContentManager content)
		{
			image = content.Load<Texture2D>("tash");
			image1 = content.Load<Texture2D>("tash");
			image2 = content.Load<Texture2D>("tashdroite");
			image3 = content.Load<Texture2D>("tashgauche");
			image4 = content.Load<Texture2D>("tashdos");


			marcheDroite.Load(content);
			marche.Load(content);
			marcheGauche.Load(content);
			marcheDos.Load(content);


		}


		public void Update(GameTime gameTime, Tilemap tilemap)

		{



			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

			int speedX = 0;
			int speedY = 0;

			if (Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				Direction = 8;
				Y -= Speed;
				speedY = -3;
				image = image4;
				marcheDos.Update(gameTime);
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				Direction = 2;
				Y += Speed;
				speedY = 3;

				image = image1;
				marche.Update(gameTime);


			}
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				Direction = 6;
				X += Speed;
				speedX = 3;

				image = image2;
				marcheDroite.Update(gameTime);
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				Direction = 4;
				X -= Speed;
				speedX = -3;
				image = image3;

				marcheGauche.Update(gameTime);
			}


			//Collision

			bool firstCollision = false;
			bool CollisionPlante = false;
			bool secondCollision = false;


			float collisionOx = X;
			float collisionOy = Y + 39;

			//Première collision

			if (Direction == 6 || Direction == 2)
			{
				collisionOx += image.Width;
				collisionOy += image.Height - 39;
			}

			int nextTileCol = (int)Math.Floor((collisionOx + speedX) / (float)tilemap.Tileset.Tilesize);
			int nextTileRow = (int)Math.Floor((collisionOy + speedY) / (float)tilemap.Tileset.Tilesize);
			Console.WriteLine(nextTileRow);
			Console.WriteLine(nextTileCol);
			int tile = tilemap.Data[nextTileRow][nextTileCol];
			if (tile == 1)
			{
				firstCollision = true;
			}


			//Collision plante
			Rectangle collisionRect = new Rectangle((int)X + speedX, (int)Y + speedY, 64, 64);


			foreach (var c in plants)
			{
					if (collisionRect.Intersects(plant.Rect))

					{
						CollisionPlante = true;
					}
					

			}

		

			//Deuxième collision
			if (Direction == 2)
			{
				collisionOx -= image.Width;
			}
			else if (Direction == 4)
			{
				collisionOy += image.Height - 39;
			}
			else if (Direction == 6)
			{
				collisionOy -= image.Height - 39;
			}
			else if (Direction == 8)
			{
				collisionOx += image.Width - 39;
			}
			nextTileCol = (int)Math.Floor((collisionOx + speedX) / (float)tilemap.Tileset.Tilesize);
			nextTileRow = (int)Math.Floor((collisionOy + speedY) / (float)tilemap.Tileset.Tilesize);
			Console.WriteLine(nextTileRow);
			Console.WriteLine(nextTileCol);
			tile = tilemap.Data[nextTileRow][nextTileCol];
			if (tile == 1)
			{
				secondCollision = true;
			}


			// Résolution

			if (firstCollision || secondCollision)

			{
				X -= speedX;
				Y -= speedY;
			}

			if (CollisionPlante == true)

			{
				X -= speedX;
				Y -= speedY;
			}

			// variable ismoving

			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				isMoving = true;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				isMoving = true;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				isMoving = true;
			}

				if (Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				isMoving = true;
			}

			if (Keyboard.GetState().IsKeyUp(Keys.Right) && (Keyboard.GetState().IsKeyUp(Keys.Down)) && (Keyboard.GetState().IsKeyUp(Keys.Left))&& (Keyboard.GetState().IsKeyUp(Keys.Up)))
			{
				isMoving = false;

			}

		}



		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{


			//		if (isMovingRight == true)
			//		{

			//			marcheDroite.X = X;
			//		marcheDroite.Y = Y;
			//		marcheDroite.Draw(gameTime, spriteBatch);
			//	}
			//else
			//{

			//isMovingRight = false;



			//base.Draw(gameTime, spriteBatch);     // <-- base.Draw appelle le sprite d'avant





			if (isMoving == true)
			{

				if (Direction == 2)
				{

					marche.X = X;
					marche.Y = Y;
					marche.Draw(gameTime, spriteBatch);
				}


				if (Direction == 6)
				{
					marcheDroite.X = X;
					marcheDroite.Y = Y;
					marcheDroite.Draw(gameTime, spriteBatch);
				}

				if (Direction == 4)
				{
					marcheGauche.X = X;
					marcheGauche.Y = Y;
					marcheGauche.Draw(gameTime, spriteBatch);
				}

				if (Direction == 8)
				{
					marcheDos.X = X;
					marcheDos.Y = Y;
					marcheDos.Draw(gameTime, spriteBatch);
				}
			}
			else
			{
				base.Draw(gameTime, spriteBatch);
			}



		}

	}

}

