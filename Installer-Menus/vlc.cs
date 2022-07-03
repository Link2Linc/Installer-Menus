using System;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Installer_Menus
{
    class Vlc
    {
        public static void AppInstall()
        {
            string homeDir = Environment.GetEnvironmentVariable("HOME");
            WebClient client = new WebClient();
            string url = "https://vlc.freemirror.org/vlc/3.0.17.3/macosx/vlc-3.0.17.3-intel64.dmg";
            string installFile = homeDir + "/VLC.dmg";
            Console.WriteLine("Starting VLC install.");
            Console.WriteLine("Saving file to " + installFile);
            DownloadFile.Downloadfile(url, installFile);
            Console.WriteLine("Finished downloading file. Mounting DMG...");
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = homeDir + "/VLC.sh",
                Arguments = "VLC.dmg " + homeDir,
            };
            Process proc = new Process()
            {
                StartInfo = startInfo,
            };
            proc.Start();

        }
    }
}