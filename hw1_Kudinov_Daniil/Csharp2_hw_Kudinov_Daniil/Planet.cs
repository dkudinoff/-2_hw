using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Game_Kudinov_Daniil
{
    /// <summary>
    /// Класс Планета. Использует свое изображение для отрисовки.
    /// Использует свою перегрузку метода Update
    /// </summary>
    class Planet : BaseObject
    {
        static Image asteroidImage = Image.FromFile(".\\img\\planet_01.png");
        Random rnd = new Random();
        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {           
            Game.Buffer.Graphics.DrawImage(asteroidImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X > (Game.Width + Size.Width))
            {
                Pos.X = 0- Size.Width;
                Pos.Y = rnd.Next(600)-150;
            }
        }
    }
}

