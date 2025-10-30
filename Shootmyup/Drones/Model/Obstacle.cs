using Shootmyup.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public static int SIZE = 100;
        private const int MIN_DISTANCE = 50; 

        public Obstacle(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Hitbox pour la collision
        public Rectangle HitBox
        {
            get { return new Rectangle(X, Y, SIZE, SIZE); }
        }

        public bool IsColliding(int px, int py)
        {
            return HitBox.Contains(px, py);
        }

        public void Render(BufferedGraphics g)
        {
            // Dessin de l'obstacle
            g.Graphics.DrawImage(Resources.obstacle, X, Y, SIZE, SIZE);

            // Dessin de la vie sous l'obstacle
            int textX = X + SIZE / 2 - 10; // centré approximativement
            int textY = Y + SIZE + 5;      // juste en dessous du sprite
            g.Graphics.DrawString(
                Health.ToString(),
                SystemFonts.DefaultFont,
                Brushes.Black,
                textX, textY
            );
        }


        public void TakeDamage(int degat)
        {
            Health -= degat;
            if (Health < 0) Health = 0;
        }
    }
}
