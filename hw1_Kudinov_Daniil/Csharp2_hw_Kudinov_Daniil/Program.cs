using System;
using System.Windows.Forms;

namespace Game_Kudinov_Daniil
{
    /// <summary>
    /// Кудинов Даниил
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 800;
            form.Height = 600;
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
