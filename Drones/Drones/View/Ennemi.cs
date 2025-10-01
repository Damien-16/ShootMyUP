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
            drawingSpace.Graphics.DrawImage(Resources.fonc, X, Y, 100, 100);
        }

      

    }
}
