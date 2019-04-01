using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game_Kudinov_Daniil
    
{   
    /// <summary>
    /// Класс астероид. Использует свое изображение для отрисовки
    /// </summary>
    class Asteroid : BaseObject
    {
        static Image asteroidImage = Image.FromFile(".\\img\\asteroid_01.png");
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {           
            Game.Buffer.Graphics.DrawImage(asteroidImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }
}
