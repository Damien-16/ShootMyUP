using Drones.Model;
using Drones.Properties;

namespace Drones
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class AirSpace : Form
    {
        public static readonly int WIDTH = 1200;        // Dimensions of the airspace
        public static readonly int HEIGHT = 600;

        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private List<Drone> fleet;


        private List<Ennemi> ennemis;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        // Initialisation de l'espace aérien avec un certain nombre de drones
        //public AirSpace(List<Drone> fleet)
        //{
        //    InitializeComponent();
        //    currentContext = BufferedGraphicsManager.Current;
        //    airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
        //    this.fleet = fleet;
        //    InitializeComponent();
        //    this.KeyPreview = true;
        //    this.KeyDown += Form1_PressedKey;
        //}
        public AirSpace(List<Drone> fleet, List<Ennemi> ennemis)
        {
            InitializeComponent();
            currentContext = BufferedGraphicsManager.Current;
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            this.fleet = fleet;
            this.ennemis = ennemis;
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form1_PressedKey;
        }


        private void Form1_PressedKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {


                case Keys.A:
                case Keys.Left:
                    foreach (Drone drone in fleet)
                    {
                        drone.setX(drone.X - 20);
                        //drone.SetImage("droneLeft.png");
                    }
                    break;

                case Keys.D:
                case Keys.Right:
                    foreach (Drone drone in fleet)
                    {

                        drone.setX(drone.X + 20);
                        //drone.SetImage("droneRight.png");
                    }
                    break;
            }

        }
        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.DrawImage(Resources.background, 0, 0, WIDTH, HEIGHT);

            // draw drones
            foreach (Drone drone in fleet)
            {
                drone.Render(airspace);
            }


            foreach (Ennemi Ennemi in ennemis)
            {
             
            }

            airspace.Render();

           
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            foreach (Drone drone in fleet)
            {
                drone.Update(interval);

                if (drone.X < -20)
                {
                    drone.setX(WIDTH - 30);
                }
                else if (drone.X > WIDTH)
                {
                    drone.setX(-20);
                }



            }
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }

        private void AirSpace_Load(object sender, EventArgs e)
        {

        }
    }
}