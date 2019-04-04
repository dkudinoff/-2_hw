using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game_Kudinov_Daniil
{
    class Bullet : BaseObject
    {
        Random rnd = new Random();
        SolidBrush Brush = new SolidBrush(Color.Yellow);
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brush, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            ///при достижении правого края экрана пуля появляется
            ///в левом крае экрана на случайной высоте (временное решение для тестов)
            if (Pos.X > Game.Width)
            {                
                Pos.X = 0;
                Pos.Y = rnd.Next(Game.Height);
            }
        }
    }
}
