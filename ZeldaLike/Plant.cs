using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike
{
    class Plant : Sprite
    {
        Texture2D image1;
        Texture2D image2;
        Texture2D image3;
        const float GROW = 0.03f;
        float planteCounter;


        public Plant(int x, int y, string path) : base(x, y, path)
        {
            Visible = true;
        }


		   
    
        int direction;

		public Plant (Hero hero): base ("plant")
        {

			direction = hero.Direction;
			if (direction == 6)
			{
				X = hero.X + 64;
				Y = hero.Y + 25;
			}
			if (direction == 2)
			{
				X = hero.X + 30;
				Y = hero.Y + 60;
			}
			if (direction == 8)
			{
				X = hero.X + 30;
				Y = hero.Y - 64;
			}
			if (direction == 4)
			{
				X = hero.X + 10;
				Y = hero.Y + 25;
			}
		}








        public override void Load(ContentManager content)
        
    {
            image1 = content.Load<Texture2D>("plant");
            image2 = content.Load<Texture2D>("plant2");
            image3 = content.Load<Texture2D>("plant3");

            image = image1;
    }
    public void Update(GameTime gameTime)
    {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            planteCounter += dt;

            if (planteCounter < 15)
            {
                image = image1;
            }

            if (planteCounter >= 15 && planteCounter <= 30)
            {
                image = image2;
            }
            if (planteCounter >30)
            {
                image = image3;
            }

        }


    }

    }
