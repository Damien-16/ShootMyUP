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
        private string _name;



        public Ennemi(int x, int y, string name)
        {
            _x = x;
            _y = y;
            _name = name;

        }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public void setX(int x) { _x = x; }
        public void setY(int y) { _y = y; }
        public string Name { get { return _name; } }


        public void Update(int interval)
        {

            _y += GlobalHelpers.alea.Next(-1, 2);
        }

    }
}
