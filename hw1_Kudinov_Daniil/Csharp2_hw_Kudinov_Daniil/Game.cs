﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace Game_Kudinov_Daniil
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static BaseObject[] _objs;

        static Game()
        {
        }

        public static void Load()
        {
            _objs = new BaseObject[100];
            for (int i =0 ; i < _objs.Length / 10 * 9; i++)
                _objs[i] = new Star(new Point(i * 20 - 800, i * 20 - 600), new Point(-i, 0), new Size(4, 4));
            _objs[_objs.Length / 10 * 9] = new Planet(new Point(-300, 50), new Point(-5, 0), new Size(300, 300));
            for (int i = _objs.Length / 10 * 9+1; i < _objs.Length; i++)
                _objs[i] = new Asteroid(new Point(600, (i-90) * 30), new Point(-i+90, -i+90), new Size(60, 60));            
            
        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static void Draw()
        {
            // Проверяем вывод графики
            //Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
