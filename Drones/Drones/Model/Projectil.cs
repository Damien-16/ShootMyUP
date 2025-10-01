using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shootmyup
{
    public partial class Projectil
    {
        private int _x;
        private int _y;

        public Projectil(int x, int y)
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

            _y -= 50;
            
        }
    }
}
