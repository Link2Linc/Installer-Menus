using System;
using System.Threading;
using System.Net;
namespace Installer_Menus
{
    class Program
    {

        //public static string app;
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Loading Binary File...");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("1/4");
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Initiating main process...");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("2/4");
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Importing Libraries....");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("3/4");
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Parsing command line arguments....");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("4/4");
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("Welcome to the Installer-Menus CLI application. Available apps are:");
            FetchApps("https://raw.githubusercontent.com/Link2Linc/assets/main/installer-menus/apps");
            Console.Write("Please type in what app you would like to install:  ");
            string app = Console.ReadLine();
            InstallApp(app);
        }
        public static void FetchApps(string url)
        {
            // Create web client.
            WebClient client = new WebClient();

            // Download string.
            string value = client.DownloadString(url);

            // Write values.
            Console.Write(value);
            Console.Write("\n\n");
        }
        public static void InstallApp(string app)
        {
            if(app == "vlc")
            {
                Console.Clear();
                Vlc.AppInstall();
            } else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The app you entered does not work, or does not exist.");
            }
        }
    }
}