using System.Text.Json;
using URLShortener.Models;

namespace URLShortener.BLL
{
    public class UrlBLL
    {
        List<Address> Addresses;

        string strPath = "";

        public UrlBLL()
        {
            strPath = Environment.CurrentDirectory + "\\Data.json";
        }

        public string GetUrl(string url)
        {
            Addresses = ReadFileAndDeserialize();

            var a = Addresses.Where(a => a.ShortURL == url).FirstOrDefault();

            return (a.LongURL);
        }

        public void SetUrl(string longUrl)
        {
            Addresses = ReadFileAndDeserialize();

            var address = new Address() { LongURL = longUrl, ShortURL = Guid.NewGuid().ToString().Substring(0, 8) };

            Addresses.Add(address);

            var p = JsonSerializer.Serialize<List<Address>>(Addresses);

            StreamWriter streamWriter = new StreamWriter(strPath, false);

            streamWriter.Write(p);

            streamWriter.Dispose();
        }

        private List<Address> ReadFileAndDeserialize()
        {
            if (File.Exists(strPath) == false)
            {
                File.Create(strPath).Dispose();
            }

            StreamReader streamReader = new StreamReader(strPath);

            var data = streamReader.ReadToEnd();

            streamReader.Dispose();

            if (string.IsNullOrEmpty(data))
                return new List<Address>();

            return JsonSerializer.Deserialize<List<Address>>(data);
        }
    }
}