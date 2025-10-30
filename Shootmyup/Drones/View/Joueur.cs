using Shootmyup.Helpers;
using Shootmyup.Properties;
using System.Resources;

namespace Shootmyup
{
      public partial class Joueur
    {

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            // Vie au-dessus du joueur
            int textX = X + 35;
            int textY = Y - 20; 
            drawingSpace.Graphics.DrawString(
                $"{Health}",
                SystemFonts.DefaultFont,
                Brushes.Red,
                textX,
                textY
            );

            drawingSpace.Graphics.DrawImage(Resources.drone, X, Y, 100, 100);
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"{Name}";
        }

    }
}
