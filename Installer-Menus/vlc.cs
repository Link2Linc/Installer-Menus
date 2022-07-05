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
            url = "https://raw.githubusercontent.com/Link2Linc/Installer-Menus/master/build-scripts/VLC.sh";
            installFile = homeDir + "/VLC.sh";
            DownloadFile.Downloadfile(url, installFile);
            Console.WriteLine("Finished downloading file. Mounting DMG...");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{"chmod +x " + homeDir + "/VLC.sh && xattr -drs com.apple.quarantine " + homeDir + "/VLC.sh"}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            Thread.Sleep(1000);
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
            Thread.Sleep(20000);
            if(Directory.Exists(homeDir + "/Applications/VLC.app"))
            {
                Console.WriteLine("VLC already exists.");
                Thread.Sleep(1500);
                Environment.Exit(0);
                
            } else
            {
                Console.WriteLine("Copying VLC from mounted DMG to local applications folder...");
                CopyDirectory("/Volumes/VLC media player/VLC.app", homeDir + "/Applications/VLC.app", true);
            }
            Console.Write("\n\n\n\n\n Successfully installed VLC. Cleaning files");
            File.Delete(homeDir + "/VLC.dmg");
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
                Arguments = "/Volumes/VLC\\ media\\ player/ " + homeDir,
            };
            Process proc1 = new Process()
            {
                StartInfo = startInfo,
            };
            proc.Start();
            Thread.Sleep(2000);
            File.Delete(homeDir + "/unmount.sh");
            File.Delete(homeDir + "VLC.sh");
            Console.Clear();
            Console.Write("\n\n finished cleaning up. Enjoy VLC!");
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