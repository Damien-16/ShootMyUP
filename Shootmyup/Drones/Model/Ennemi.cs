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
        private int _x;
        private int _y;
        public int dir ;


        public Ennemi(int x, int y)
        {
            _x = x;
            _y = y;

        }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public void setX(int x) { _x = x; }
        public void setY(int y) { _y = y; }


        public void Update(int interval)
        {
            if (dir==1)
            {
                _x +=10;
            }
            else
            {
                _x -= 10;
            }

        }
        

    }
}
