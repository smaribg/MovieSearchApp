using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDownload
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using MovieSearch;

    public class ImageDownloader
    {
        private IImageStorage _imageStorage;

        public ImageDownloader(IImageStorage imageStorage)
        {
            this._imageStorage = imageStorage;
        }

        public string LocalPathForFilename(string remoteFilePath)
        {
            if (remoteFilePath == null)
            {
                return string.Empty;
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localPath = Path.Combine(documentsPath, remoteFilePath.TrimStart('/'));
            return localPath;
        }


        public async Task DownloadImage(string remoteFilePath, string localFilePath, CancellationToken token)
        {
            var fileStream = new FileStream(
                                 localFilePath,
                                 FileMode.Create,
                                 FileAccess.Write,
                                 FileShare.None,
                                 short.MaxValue,
                                 true);
            try
            {
                await this._imageStorage.DownloadAsync(remoteFilePath, fileStream, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task DownloadImages(List<Movie> movies){
            foreach(var movie in movies){
                var poster = movie.ImageRemote;
                var localPoster = this.LocalPathForFilename(poster);
                if(localPoster != string.Empty && !File.Exists(localPoster)){
                    await this.DownloadImage(poster, localPoster, new CancellationToken());

                }
                movie.ImageLocal = localPoster;
            }
        }

        public async Task DownloadBackdrops(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                var poster = movie.BackdropRemote;
                var localPoster = this.LocalPathForFilename(poster);
                if (localPoster != string.Empty && !File.Exists(localPoster))
                {
                    await this.DownloadImage(poster, localPoster, new CancellationToken());

                }
                movie.BackdropLocal = localPoster;
            }
        }

    }
}
