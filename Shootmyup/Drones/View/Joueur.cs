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
            drawingSpace.Graphics.DrawImage(Resources.drone, X, Y, 100, 100);
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"{Name}";
        }

    }
}
