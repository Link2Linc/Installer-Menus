using System;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Installer_Menus
{
    class Obs
    {
        public static void AppInstall()
        {
            string homeDir = Environment.GetEnvironmentVariable("HOME");
            WebClient client = new WebClient();
            string url = "https://cdn-fastly.obsproject.com/downloads/obs-mac-27.2.4.dmg";
            string installFile = homeDir + "/OBS.dmg";
            Console.WriteLine("Starting OBS install.");
            Console.WriteLine("Saving file to " + installFile);
            DownloadFile.Downloadfile(url, installFile);
            url = "https://raw.githubusercontent.com/Link2Linc/Installer-Menus/master/build-scripts/unmount.sh";
            installFile = homeDir + "/unmount.sh";
            DownloadFile.Downloadfile(url, installFile);
            Console.WriteLine("Finished downloading file. Mounting DMG...");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{"chmod +x " + homeDir + "/mount.sh && xattr -drs com.apple.quarantine " + homeDir + "/mount.sh"}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            Thread.Sleep(1000);
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = homeDir + "/mount.sh",
                Arguments = "OBS.dmg " + homeDir,
            };
            Process proc = new Process()
            {
                StartInfo = startInfo,
            };
            proc.Start();
            Thread.Sleep(20000);
            if (Directory.Exists(homeDir + "/Applications/OBS.app"))
            {
                Console.WriteLine("OBS already exists.");
                Thread.Sleep(1500);
                Environment.Exit(0);

            }
            else
            {
                Console.WriteLine("Copying OBS from mounted DMG to local applications folder...");
                CopyDirectory("/Volumes/OBS-Studio 27.2.4/OBS.app", homeDir + "/Applications/OBS.app", true);
            }
            Console.Write("\n\n\n\n\n Successfully installed OBS. Cleaning files");
            File.Delete(homeDir + "/OBS.dmg");
            url = "https://raw.githubusercontent.com/Link2Linc/Installer-Menus/master/build-scripts/unmount.sh";
            installFile = homeDir + "/unmount.sh";
            DownloadFile.Downloadfile(url, installFile);
            Thread.Sleep(2000);
            process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{"chmod +x " + homeDir + "/unmount.sh && xattr -drs com.apple.quarantine " + homeDir + "/unmount.sh"}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            ProcessStartInfo startInfo1 = new ProcessStartInfo()
            {
                FileName = homeDir + "/unmount.sh",
                Arguments = "/Volumes/OBS-Studio\\ 27.2.4/ " + homeDir,
            };
            Process proc1 = new Process()
            {
                StartInfo = startInfo,
            };
            proc.Start();
            Thread.Sleep(2000);
            File.Delete(homeDir + "/unmount.sh");
            File.Delete(homeDir + "/mount.sh");
            Console.Clear();
            Console.Write("\n\n finished cleaning up. Enjoy OBS!");
        }
        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
    }
}