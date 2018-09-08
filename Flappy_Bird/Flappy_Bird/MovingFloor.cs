using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Flappy_Bird
{
    class MovingFloor
    {
        private Texture2D _texture;
        private float _speed;
        private Rectangle floor1;
        private Rectangle floor2;
        private int floor_h, floor_w;
        public int _W, _H;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        //public Texture2D Texture
        //{
        //    get { return _texture; }
        //    set { _texture = value; }
        //}

        //public int Width
        //{
        //    get { return _W; }
        //}

        //public int Height
        //{
        //    get { return _H; }
        //}


        public void Initialize(Texture2D texture, float speed, int screen_w, int screen_h)
        {
            _texture = texture;
            _speed = speed;
            floor_h = _texture.Height;
            floor_w = _texture.Width;
            _W = screen_w;
            _H = screen_h;
            floor1 = new Rectangle(0, 0, floor_w, floor_h);
            floor2 = new Rectangle(floor_w, 0, floor_w, floor_h);
        }

        public void Update()
        {
            // update backgroud
            if (floor1.X <= 0 - floor_w)
            {
                floor1.X = floor_w;
            }
            if (floor2.X <= 0 - floor_w)
            {
                floor2.X = floor_w;
            }

            floor1.X = (int)(floor1.X + _speed);
            floor2.X = (int)(floor2.X + _speed);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, floor1, Color.White);
            sb.Draw(_texture, floor2, Color.White);
        }
    }
}
