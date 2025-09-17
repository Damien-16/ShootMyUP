using Drones.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public partial class Building
    {
        private Pen BuildingBrush = new Pen(new SolidBrush(Color.Red), 3);

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawEllipse(BuildingBrush, new Rectangle(X - 4, Y - 2, height, deep));
        }



    }
}
