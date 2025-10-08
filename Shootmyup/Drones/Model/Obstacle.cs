using Shootmyup.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shootmyup.Model
{
    public partial class Obstacle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; } = 10;
        public static int SIZE = 70;

        public Obstacle(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Render(BufferedGraphics g)
        {
            g.Graphics.DrawImage(Resources.obstacle, X, Y, 50, -50);

            
            g.Graphics.DrawString(
                Health.ToString(),SystemFonts.DefaultFont,Brushes.Black,X, Y);
        }

        public void TakeDamage(int degat)
        {
            Health -= degat;
            if (Health < 0) Health = 0;
        }

    }
}
