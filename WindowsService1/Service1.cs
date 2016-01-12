using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        static TwitterService service = null;
        static string downloadPath = "D:\\Cosicas D";
        static string torrentPath = "C:\\Users\\e.alvir\\Desktop\\pruebasEventos";
        protected override void OnStart(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                torrentPath = args[0];
                if (args.Length > 1)
                {
                    downloadPath = args[1];
                }
            }

            if (File.Exists(downloadPath))
            {
                createDownloadWatcher();
            }
            if (File.Exists(torrentPath))
            {
                createTorrentWatcher();
            }

            // Twitter
            initTwitter();
        }
        private static void initTwitter()
        {
            service = new TwitterService("Zu9eMYImenEPW3WA7Z6zs6eAL", "it8ezCqWXD8TjC8Sbc8PfoJHvTdtdhn26DuQcnufIbzPDBIHgM");
            service.AuthenticateWith("102982151-hGLzg7DRFOpo4QtujwqfLBG9kjyLnj0rZAWBwzjI", "D8qKQmmVCyrvWG24mBSSzIHr3ZojFrgdvbMTVYTtnwTyT");
        }

        private static void createDownloadWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = downloadPath;

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Filter = "*.*";
            watcher.Created += new FileSystemEventHandler(OnCreatedDownload);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private static void createTorrentWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = torrentPath;

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Filter = "*.*";
            watcher.Created += new FileSystemEventHandler(OnCreatedTorrent);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }


        // Define the event handlers.
        private static void OnCreatedDownload(object source, FileSystemEventArgs e)
        {
            service.SendTweet(new SendTweetOptions { Status = e.Name + " se ha descargado!" });
        }

        // Define the event handlers.
        private static void OnCreatedTorrent(object source, FileSystemEventArgs e)
        {
            service.SendTweet(new SendTweetOptions { Status = "Agregado " + e.Name + " !" });
        }

        protected override void OnStop()
        {
        }

    }
}
