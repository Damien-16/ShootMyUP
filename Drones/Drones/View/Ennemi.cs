using Drones.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.View
{
    public partial class Ennemi
    {

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.drone, 100, 100, 100, 100);
        }



    }
}
