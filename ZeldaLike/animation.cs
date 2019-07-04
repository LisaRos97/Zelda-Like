using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike
{
    class Animation : Sprite
    {
        public Animation(string path) : base(path)

        {
            Visible = true;
        }

        int frame = 0;
        float changeTime = 0.1f;
        float animCounter;

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            animCounter += dt;

            if (animCounter > changeTime)
            {
                frame = frame + 1;

                animCounter = 0;

                if (frame > 5)
                {
                    frame = 0;
                }
            }


        }


		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                Rectangle drawRect = new Rectangle(
                       frame * 64, 0, 64, image.Height);


                Rectangle rect = new Rectangle((int)X, (int)Y, 64, image.Height);
                spriteBatch.Draw(image, rect, drawRect, Color, Rotation, new Vector2(Ox, Oy), SpriteEffects.None, 0);


            }
        }


    }
}
