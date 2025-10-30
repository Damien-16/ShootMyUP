using Shootmyup.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shootmyup
{
    public partial class Ennemi
    {

        public void Render(BufferedGraphics drawingSpace)
        {
            // Dessin de l’image de l’ennemi
            drawingSpace.Graphics.DrawImage(Resources.ennemi, X, Y, SIZE, SIZE);

            // Dessin des cœurs sous l’ennemi
            int heartSize = 20;
            int startX = X + (SIZE / 2) - ((Health * heartSize) / 2);
            int heartY = Y + SIZE - 10; // sous l’ennemi

            for (int i = 0; i < Health; i++)
            {
                drawingSpace.Graphics.DrawImage(Resources.heart, startX + i * heartSize, heartY, heartSize, heartSize);
            }
        }



    }
}
