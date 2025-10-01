
namespace Shootmyup
{
    public partial class Joueur
    {
      
        private string _name;                           // Un nom
        private int _x;                                 // Position en X depuis la gauche de l'espace aérien
        private int _y;                                 // Position en Y depuis le haut de l'espace aérien

        // Constructeur
        public Joueur(int x, int y, string name)
        {
            _x = x;
            _y = y;
            _name = name;
        }
        public int X { get { return _x;} }
        public int Y { get { return _y;} }
        public void setX(int x) { _x = x; }
        public void setY(int y) { _y = y; }
        public string Name { get { return _name;} }
        
       


       
        public void Update(int interval)
        {
                                             
            //_y += GlobalHelpers.alea.Next(-1, 2);    
        }

        internal void SetImage(string v)
        {
            throw new NotImplementedException();
        }
    }

}
