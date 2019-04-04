using System;
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
        private static Asteroid[] _asteroids;
        private static Planet[] _planets;
        private static Bullet _bullet;

        static Game()
        {
        }

        public static void Load()
        {
            _objs = new BaseObject[120];
            _bullet = new Bullet(new Point(0, 720), new Point(20, 0), new Size(40, 10));
            _asteroids = new Asteroid[20];
            _planets = new Planet[1];
            var rnd = new Random();

            ///Загрузка звезд
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 100);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r/4, r/4), new Size(1, 1));
            }
            ///Загрузка планеты-декорации
            for (var i = 0; i < _planets.Length; i++)
            {
                _planets[i] = new Planet(new Point(-300, 50), new Point(-1, 0), new Size(400, 400));
            }
            ///Загрузка астероидов
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5,50);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height)), new Point(-100 / r, 100/r), new Size(r*3/2+15, r*3/2+15));
            }      
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
            ///Width = form.ClientSize.Width;   ///временно отключена ширина во весь экран (>1000)
            ///Height = form.ClientSize.Height; ///временно отключена высота во весь экран (>1000)
            Width = 1000;
            Height = 700;
            ///параметры для теста работы исключения
            ///Width = -5;
            ///Height = 1200;
            //Выбрасываем исключение в случае некорректных данных высоты или ширины экрана
            if (Width > 1000 || Width < 0) throw new ArgumentOutOfRangeException("Недопустимая ширина экрана!");
            if (Height > 1000 || Height < 0) throw new ArgumentOutOfRangeException("Недопустимая высота экрана!");
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 10 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Planet p in _planets)
                p.Draw();
            foreach (Asteroid a in _asteroids)
                a.Draw();
            _bullet.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    ///при столкновении с астероидом, телепортируем пулю в левый край экрана,
                    ///а астероид - в правый край.
                    _bullet.Pos.X = 0;
                    a.Pos.X = Game.Width;
                }               
                ///попытка обработать столкновения астероидов с астероидами
                ///(работает плохо - астероиды застревают друг в друге)
                //foreach (Asteroid a2 in _asteroids)
                //{
                //    if (a.Collision(a2))
                //    {
                //        a2.Dir.X = -a2.Dir.X;
                //        a2.Dir.Y = -a2.Dir.Y;
                //        a.Dir.X = -a.Dir.X;
                //        a.Dir.Y = -a.Dir.Y;
                //    }
                //}
            }
            foreach (Planet p in _planets)
                p.Update();
            _bullet.Update();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
