using Shootmyup.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shootmyup
{
    public partial class Projectil
    {
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.fireball, X, Y, 50, -50);
        }
    }
}
