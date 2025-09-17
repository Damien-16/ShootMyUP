using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public partial class Building
    {
        Random r = new Random();

        private int _x;
        private int _y;
        private const int height = 100;
        private const int deep = 100;
        private string _color;

        public int X
        {
            get
            {
                return X1;
            }
            set
            {
                X1 = value;
            }

        }
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }

        }

        public int X1 { get => _x; set => _x = value; }
        public int Y1 { get => _y; set => _y = value; }

    }
}
