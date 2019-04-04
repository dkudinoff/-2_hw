using System;
using System.Drawing;

namespace Game_Kudinov_Daniil
{
    public abstract class BaseObject : ICollision
    {
        
        public Point Pos;
        public Point Dir;
        public Size Size;


        protected BaseObject(Point pos, Point dir, Size size) 
        { 
            Pos = pos;
            Dir = dir;
            Size = size;

            ///Обработка исключения при некорректном размере объекта
            if (Size.Height > Game.Height ||
                Size.Height < 0 ||
                Size.Width > Game.Width ||
                Size.Width < 0 ||
                Size.Height*Size.Width>Game.Height*Game.Width/3
                )
                throw new GameObjectException("Ошибка! Некорректный размер объекта!");

            ///Обработка исключения при некорректной скорости объекта
            if (Dir.X>Game.Width/20 ||
                Dir.Y>Game.Height/20 ||
                Dir.X < -Game.Width / 20 ||
                Dir.Y < -Game.Height / 20 ||
                Math.Sqrt(Dir.X*Dir.X+Dir.Y*Dir.Y)> Math.Sqrt(Game.Width * Game.Width + Game.Height * Game.Height)/20 ||
                Math.Sqrt(Dir.X*Dir.X+Dir.Y*Dir.Y)< -Math.Sqrt(Game.Width * Game.Width + Game.Height * Game.Height)/20
                )
                throw new GameObjectException("Ошибка! Некорректная скорость объекта!");
        }

        public abstract void Draw();

        public abstract void Update();

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);

    }

}
