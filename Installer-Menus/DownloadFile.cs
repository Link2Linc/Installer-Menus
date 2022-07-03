using System.Net;

namespace Installer_Menus
{
    class DownloadFile
    {
        public static void Downloadfile(string url, string path)
        {
            WebClient Client = new WebClient();
            Client.DownloadFile(url, path);
        }
    }
}