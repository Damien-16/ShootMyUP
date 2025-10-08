using Shootmyup;
using Shootmyup.Model;
using Shootmyup.Properties;
using System.Reflection;

namespace Shootmyup
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class AirSpace : Form
    {
        public static readonly int WIDTH = 1200;        // Dimensions of the airspace
        public static readonly int HEIGHT = 600;

        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private List<Joueur> fleet;

        private List<Ennemi> ennemis;
        private List<Projectil> projectils;
        private List<Obstacle> obstacles = new List<Obstacle>();
        private Random rand = new Random();




        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        public AirSpace(List<Joueur> fleet, List<Ennemi> ennemis, List<Projectil> projectils,List<Obstacle>obstacles)
        {
            InitializeComponent();
            currentContext = BufferedGraphicsManager.Current;
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            this.projectils = projectils;
            this.obstacles = obstacles;
            for (int i = 0; i < 5; i++)
            {
                int x = rand.Next(50, WIDTH - 100);
                int y = rand.Next(100, HEIGHT - 150);
                obstacles.Add(new Obstacle(x, y));
            }

            this.fleet = fleet;
            this.ennemis = ennemis;
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form1_PressedKey;
        }


        private void Form1_PressedKey(object sender, KeyEventArgs e)
        {
            foreach (Joueur joueur in fleet)
            {


                switch (e.KeyCode)
                {


                    case Keys.A:
                    case Keys.Left:
                        foreach (Joueur drone in fleet)
                        {
                            drone.setX(drone.X - 20);
                        }
                        break;

                    case Keys.D:
                    case Keys.Right:
                        foreach (Joueur drone in fleet)
                        {

                            drone.setX(drone.X + 20);
                        }
                        break;

                    case Keys.Space:
                        projectils.Add(new Projectil(joueur.X + 25, joueur.Y + 60));
                        {

                        }
                        break;
                    case Keys.Escape:
                        {
                            this.Close();
                        }
                        break;

                }
            }
        }
        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.DrawImage(Resources.background, 0, 0, WIDTH, HEIGHT);

            // draw drones
            foreach (Joueur drone in fleet)
            {
                drone.Render(airspace);
            }


            foreach (Ennemi Ennemi in ennemis)
            {
                Ennemi.Render(airspace);
            }
            foreach (Projectil projectil in projectils)
            {
                projectil.Render(airspace);
            }

            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Render(airspace);
            }
            if (obstacles.Count < 5)
            {
                
                    int x = rand.Next(50, WIDTH - 100);
                    int y = rand.Next(100, HEIGHT - 150);
                    obstacles.Add(new Obstacle(x, y));
                
            }

            airspace.Render();


        }
        
        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            foreach (Joueur drone in fleet)
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

            foreach (Ennemi ennemis in ennemis)
            {
                ennemis.Update(interval);

                if (ennemis.X < -20)
                {
                    int dir = ennemis.dir = 1;
                }
                else if (ennemis.X > WIDTH - 40)
                {
                    ennemis.dir = 2;
                }
            }
            foreach (Projectil projectil in projectils)
            {
                projectil.Update(interval);
            }
            foreach (var projectil in projectils.ToList())
            {
                projectil.Update(interval);

                foreach (var obstacle in obstacles.ToList())
                {
                    if (projectil.X >= obstacle.X &&
                        projectil.X <= obstacle.X + Obstacle.SIZE &&
                        projectil.Y >= obstacle.Y &&
                        projectil.Y <= obstacle.Y + Obstacle.SIZE)
                    {
                        obstacle.TakeDamage(1);
                        projectils.Remove(projectil);

                        if (obstacle.Health <= 0)
                            obstacles.Remove(obstacle);

                        break;
                    }
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