using Shootmyup;

namespace Shootmyup
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Création de la flotte de drones
            List<Joueur> fleet= new List<Joueur>();
            fleet.Add(new Joueur(AirSpace.WIDTH / 2, AirSpace.HEIGHT - 100, "Player"));


            List<Ennemi> ennemis= new List<Ennemi>();
            ennemis.Add(new Ennemi(AirSpace.WIDTH / 2-20, 10));

            List<Projectil> projectils= new List<Projectil>();

            // Démarrage
            Application.Run(new AirSpace(fleet, ennemis, projectils));
        }
    }
}