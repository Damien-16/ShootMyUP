using Shootmyup;
using Shootmyup.Model;
using Shootmyup.Properties;
using System.Reflection;

namespace Shootmyup
{
    // La classe AirSpace représente le territoire au-dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis au-dessus
    // Il n'y a donc pas de notion d'altitude qui intervient
    public partial class AirSpace : Form
    {
        public static readonly int WIDTH = 1600;  
        public static readonly int HEIGHT = 900;

        private const int MIN_DISTANCE = 200;

        private List<Joueur> joueurs;
        private List<Ennemi> ennemis;
        private List<Projectil> projectils;
        private List<Obstacle> obstacles;
        private Random rand = new Random();

        //projectils
        private int maxProjectiles = 7;
        private int currentProjectiles = 7;
        private DateTime lastReloadTime = DateTime.Now;
        //projectils ennemis
        private const int reloadDelay = 1000;
        //score
        private int score = 0;

        //ennemi 1
        private bool firstEnemyKilled = false;


        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        public AirSpace(List<Joueur> fleet, List<Ennemi> ennemis, List<Projectil> projectils, List<Obstacle> obstacles)
        {
            InitializeComponent();
            currentContext = BufferedGraphicsManager.Current;
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            this.projectils = projectils;
            this.obstacles = obstacles;
            this.joueurs = fleet;
            this.ennemis = ennemis;

            // Génération obstacles
            int attempts = 0;
            for (int i = 0; i < 5; i++)
            {
                bool validPosition = false;
                int x = 0, y = 0;

                while (!validPosition && attempts < 100)
                {
                    attempts++;
                    x = rand.Next(50, WIDTH - 100);
                    y = rand.Next(200, HEIGHT - 200);

                    validPosition = true;
                    foreach (var obs in this.obstacles)
                    {
                        double distance = Math.Sqrt(Math.Pow(x - obs.X, 2) + Math.Pow(y - obs.Y, 2));
                        if (distance < Obstacle.SIZE + MIN_DISTANCE)
                        {
                            validPosition = false;
                            break;
                        }
                    }
                }

                if (validPosition)
                    this.obstacles.Add(new Obstacle(x, y));
            }

            this.KeyPreview = true;
            this.KeyDown += Form1_PressedKey;
        }

        //generation des ennemis
        private Ennemi GenerateRandomEnnemi()
        {
            int x = rand.Next(50, WIDTH - 100);
            int y = rand.Next(10, 100); 
            return new Ennemi(x, y);
        }

        private void Form1_PressedKey(object sender, KeyEventArgs e)
        {
            foreach (Joueur joueur in joueurs)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                    case Keys.Left:
                        joueur.setX(joueur.X - 20);
                        break;

                    case Keys.D:
                    case Keys.Right:
                        joueur.setX(joueur.X + 20);
                        break;

                    case Keys.Space:
                        if (currentProjectiles > 0)
                        {
                            projectils.Add(new Projectil(joueur.X + 25, joueur.Y + 60, -1));
                            currentProjectiles--;
                        }
                        break;

                    case Keys.Escape:
                        this.Close();
                        break;
                }
            }
        }

       
        private void Render()
        {
            airspace.Graphics.DrawImage(Resources.background, 0, 0, WIDTH, HEIGHT);

            // Joueurs
            foreach (Joueur drone in joueurs)
                drone.Render(airspace);

            // Ennemis
            foreach (Ennemi ennemi in ennemis)
                ennemi.Render(airspace);

            // Projectiles
            foreach (Projectil projectil in projectils)
                projectil.Render(airspace);

            // Obstacles
            foreach (Obstacle obstacle in obstacles)
                obstacle.Render(airspace);

            // Régénération si moins de 5 obstacles
            if (obstacles.Count < 5)
            {
                int x = rand.Next(50, WIDTH - 100);
                int y = rand.Next(100, HEIGHT - 150);
                obstacles.Add(new Obstacle(x, y));
            }
            airspace.Graphics.DrawString(
                $"Score : {score}",
                new Font("Arial", 16, FontStyle.Bold),
                Brushes.White,
                20, 20
            );
            airspace.Graphics.DrawString(
                $"Munitions : {currentProjectiles}/{maxProjectiles}",
                new Font("Arial", 16, FontStyle.Bold),
                Brushes.Black,
                20, 50
            );
            airspace.Render();
        }

        
        private void Update(int interval)
        {
            //Recharge projectils
            if ((DateTime.Now - lastReloadTime).TotalMilliseconds >= reloadDelay)
            {
                if (currentProjectiles < maxProjectiles)
                {
                    currentProjectiles++;
                    lastReloadTime = DateTime.Now;
                }
            }
            //Joueurs
            foreach (Joueur joueur in joueurs)
            {
                joueur.Update(interval);
                if (joueur.X < -20)
                    joueur.setX(WIDTH - 30);
                else if (joueur.X > WIDTH)
                    joueur.setX(-20);
            }

            //Ennemis
            foreach (Ennemi ennemi in ennemis.ToList())
            {
                ennemi.Update(interval);

                if (ennemi.X < -20)
                    ennemi.dir = 1;
                else if (ennemi.X > WIDTH - 40)
                    ennemi.dir = 2;

                // Tir ennemi
                Projectil newShot = ennemi.TryFire(interval);
                if (newShot != null)
                    projectils.Add(newShot);
            }

            //Projectiles 
            foreach (Projectil projectil in projectils.ToList())
            {
                projectil.Update(interval);

                // Collision projectile obstacle
                foreach (var obstacle in obstacles.ToList())
                {
                    if (obstacle.IsColliding(projectil.X, projectil.Y))
                    {
                        obstacle.TakeDamage(1);
                        projectils.Remove(projectil);

                        if (obstacle.Health <= 0)
                            obstacles.Remove(obstacle);
                        break;
                    }
                }

                // Collision projectile ennemi - joueur
                foreach (var joueur in joueurs.ToList())
                {
                    if (projectil.Direction == 1 && // tirer en bas
                        projectil.X >= joueur.X &&
                        projectil.X <= joueur.X + 100 &&
                        projectil.Y >= joueur.Y &&
                        projectil.Y <= joueur.Y + 100)
                    {
                        joueur.TakeDamage(1);
                        projectils.Remove(projectil);
                        break;
                    }
                }

                // Collision projectile - ennemi
                foreach (var ennemi in ennemis.ToList())
                {
                    if (projectil.Direction == -1 && // tir vers le haut
                        projectil.X >= ennemi.X &&
                        projectil.X <= ennemi.X + 100 &&
                        projectil.Y >= ennemi.Y &&
                        projectil.Y <= ennemi.Y + 100)
                    {
                        ennemi.TakeDamage(1);
                        projectils.Remove(projectil);
                        if (ennemi.Health <= 0)
                        {
                            ennemis.Remove(ennemi);

                            score += 100;
                            if (!firstEnemyKilled)
                            {
                                firstEnemyKilled = true;

                                // Faire apparaître 3 nouveaux ennemis
                                for (int i = 0; i < 3; i++)
                                {
                                    ennemis.Add(GenerateRandomEnnemi());
                                }
                            }
                            else
                            {
                                // Après le premier kill, spwan quand un meurt
                                ennemis.Add(GenerateRandomEnnemi());
                            }
                        }
                        break;
                    }
                }
            }



            // Collisions joueur - obstacle 
            foreach (var joueur in joueurs.ToList())
            {
                foreach (var obstacle in obstacles)
                {
                    if (joueur.X < obstacle.X + Obstacle.SIZE &&
                        joueur.X + 100 > obstacle.X &&
                        joueur.Y < obstacle.Y + Obstacle.SIZE &&
                        joueur.Y + 100 > obstacle.Y)
                    {
                        joueur.TakeDamage(1);
                    }
                }
            }

            // Suppression des joueurs morts
            foreach (var joueur in joueurs.ToList())
            {
                if (joueur.Health <= 0)
                {
                    joueurs.Remove(joueur);
                    ticker.Stop();
                    MessageBox.Show("Partie terminée ! Votre score : " + score, "Fin de la partie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }

            }
        }

        
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }

        private void AirSpace_Load(object sender, EventArgs e) { }
    }
}
