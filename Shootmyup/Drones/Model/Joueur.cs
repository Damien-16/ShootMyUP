using Shootmyup.Properties;
using System.Drawing;

namespace Shootmyup
{
    public partial class Joueur
    {
        private string _name;
        private int _x;
        private int _y;

        public int Health { get; private set; } = 3; 
        public Joueur(int x, int y, string name)
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

        public void TakeDamage(int dmg)
        {
            Health -= dmg;
            if (Health < 0) Health = 0;
        }

        public void Update(int interval)
        {
         
        }

        
    }
}
