using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Flappy_Bird
{
    class Pillar
    {
        public int W, H;
        public int speed;
        public Texture2D pic;
        public Vector2 position;
        Random randomGenerator;
        public List<Vector2> Stonelist = new List<Vector2>();
        public int putStone;


        public void Initialize()
        {
            putStone = 0;
            speed = 5;
        }
        public void Update(GameTime gameTime)
        {
            putStone += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (putStone >= 1500)
            {
                putStone = 0;
                randomGenerator = new Random();
                int Space = randomGenerator.Next(5, 6);
                int StoneSort = randomGenerator.Next(4, 7);

                for (int i = 0; i < StoneSort; i++)
                {
                    position.X = W;
                    position.Y = pic.Height * i;
                    Stonelist.Add(position);
                }
                for (int a = StoneSort + Space; a <= 20; a++)
                {
                    position.X = W;
                    position.Y = pic.Height * a;
                    Stonelist.Add(position);
                }
            }
            // move Stone
            List<Vector2> stonemove = new List<Vector2>();
            for (int i = 0; i < Stonelist.Count; i++)
            {
                Vector2 oldPos = Stonelist[i];
                Vector2 newPos;
                newPos.Y = oldPos.Y;
                newPos.X = oldPos.X - speed;
                if (newPos.X > -pic.Width)
                {
                    stonemove.Add(newPos);
                }
            }
            Stonelist = stonemove;
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (Vector2 Pos in Stonelist)
            {
                sb.Draw(pic, Pos, Color.White);
            }
        }
    }
}
