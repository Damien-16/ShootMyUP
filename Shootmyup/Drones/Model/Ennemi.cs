using Shootmyup.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shootmyup
{
    public partial class Ennemi
    {
        private int _x;
        private int _y;
        public int dir;
        public int Health { get; set; } = 3;  // points de vie
        public static readonly int SIZE = 100;
        public int FireCooldown { get; set; } = 1000; 
        private int timeSinceLastShot = 0;           


        public Ennemi(int x, int y)
        {
            _x = x;
            _y = y;
            dir = 1; // direction initiale
        }

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public void setX(int x) { _x = x; }
        public void setY(int y) { _y = y; }

        public void TakeDamage(int dmg)
        {
            Health -= dmg;
            if (Health < 0) Health = 0;
        }

        public void Update(int interval)
        {
            _y += GlobalHelpers.alea.Next(-1, 2); 

            if (dir == 1)
                _x += 10;
            else
                _x -= 10;
        }
        public Projectil TryFire(int interval)
        {
            timeSinceLastShot += interval;

            if (timeSinceLastShot >= FireCooldown)
            {
                timeSinceLastShot = 0;
                // le projectile part du centre de l’ennemi
                return new Projectil(X + SIZE / 2 - 25, Y + SIZE+100,+1); 
            }

            return null; // pas encore prêt à tirer
        }


    }
}
