using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AsyncApplication
{
    public class SaveWebImageAsync
    {
        private readonly int _threadNumber;
        private int _saveIterator = 1;
        
        public SaveWebImageAsync(int threadNumber)
        {
            this._threadNumber = threadNumber;
        }

        public async Task DoAsync(List<String> websiteList)
        {
            foreach (String websiteItem in websiteList)
            {
                var website = websiteItem;

                String html = await ReadWebsiteAsync(website);
                List<String> imageList = await GetWebsiteImageListAsync(html);

                foreach (String imageItem in imageList)
                {
                    var image = imageItem;
                    await Task.Run(() => this.SaveImage(website, image));
                }
            }
        }

        public async Task<String> ReadWebsiteAsync(String website)
        {
            var client = new WebClient();
            return  await Task.Run(() => client.DownloadString(website));
        }

        public async Task<List<String>> GetWebsiteImageListAsync(string html)
        {
            List<String> imageList = new List<string>();
            foreach (Match m in Regex.Matches(html, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                string src = m.Groups[1].Value;
                imageList.Add(src);
            }

            return imageList;
        }

        private void SaveImage(String website, String imageUrl)
        {
            var saveDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SaveImage\\" + this._threadNumber;
            if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);

            if (imageUrl.ToLower().EndsWith(".jpg") || imageUrl.ToLower().EndsWith(".jpeg") || imageUrl.ToLower().EndsWith(".git") || imageUrl.ToLower().EndsWith(".png"))
            {
                //trata se o nome do arquivo é muito grande
                var nomeArquivo = Path.GetFileName(imageUrl);
                if (nomeArquivo.Length > 30) nomeArquivo = nomeArquivo.Substring(nomeArquivo.Length - 20);

                //verifica se o caminho da imagem é relativo
                if (!imageUrl.Contains("http")) imageUrl = website + imageUrl;

                try
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(imageUrl, saveDirectory + "\\" + nomeArquivo);

                    Util.WriteLog("\t " + this._saveIterator + " Image saved", this._threadNumber);
                    this._saveIterator++;
                }
                catch { }
            }
        }
    }
}
