using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Drones.Model
{
    public class Ennemi
    {
        private int _x;                    
        private int _y;



        public Ennemi(int x, int y, string name)
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

            _y += GlobalHelpers.alea.Next(-1, 2);    
        }

    }
}
