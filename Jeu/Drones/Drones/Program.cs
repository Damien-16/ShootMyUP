namespace Drones
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
            List<Drone> fleet= new List<Drone>();
            fleet.Add(new Drone(AirSpace.WIDTH / 2, AirSpace.HEIGHT / 2, "Joe"));
            fleet.Add(new Drone(AirSpace.WIDTH / 3, AirSpace.HEIGHT / 3, "BOB2"));
            fleet.Add(new Drone(AirSpace.WIDTH / 4, AirSpace.HEIGHT / 4, "BOB3"));
            fleet.Add(new Drone(AirSpace.WIDTH / 5, AirSpace.HEIGHT / 5, "BOB4"));



            // Démarrage
            Application.Run(new AirSpace(fleet));
        }
    }
}