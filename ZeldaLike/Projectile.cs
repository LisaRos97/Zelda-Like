using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike
{
    class Projectile : Sprite

    
    {
        int direction;
    
        public Projectile (Hero hero): base ("projectile")
        {
            
            direction = hero.Direction;
            if (direction ==6)
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
                Y = hero.Y -64;
            }
            if (direction == 4)
            {
                X = hero.X + 10;
                Y = hero.Y +25 ;
            }
        }
        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
           if (direction == 6)
            {
                X = X + 1000 * dt;
            }
            if (direction == 4)
            {
                X = X - 1000 * dt;
            }
            if (direction == 8)
            {
                Y = Y - 1000 * dt;
            }
            if (direction == 2)
            {
                Y = Y + 1000 * dt;
            }

        }
    }
}
