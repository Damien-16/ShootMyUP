using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shootmyup.Properties;

namespace Shootmyup
{
    public partial class Projectil
    {
        private int _x;
        private int _y;
        private int _direction; // -1 = vers le haut (joueur), 1 = vers le bas (ennemi)

        // Constructeur
        public Projectil(int x, int y, int direction = -1)
        {
            _x = x;
            _y = y;
            _direction = direction;
        }

        // Getters / Setters
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public int Direction { get { return _direction; } } // <- direction publique
        public void setX(int x) { _x = x; }
        public void setY(int y) { _y = y; }

        // Mise à jour de la position
        public void Update(int interval)
        {
            _y += 70 * _direction;
        }

        // Affichage du projectile
        
    }
}
