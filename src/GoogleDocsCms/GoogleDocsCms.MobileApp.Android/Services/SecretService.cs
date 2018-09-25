using System;
using System.IO;
using GoogleDocsCms.MobileApp.Interfaces;

namespace GoogleDocsCms.MobileApp.Droid.Services
{
    public class SecretService : ISecretService
    {
        // AFAIK Android does not allow access to files. So I add an asset file and copy it to a temp dir to access it
        public string GetSecretFilePath()
        {
            var fileName = "secret.json";

            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var newFileName = Path.Combine(path, fileName);

            try
            {
                if (!File.Exists(newFileName))
                {
                    using (var stream = Android.App.Application.Context.Assets.Open(fileName))
                    {
                        using (var newFileStream = new BinaryWriter(new FileStream(newFileName, FileMode.Create)))
                        {
                            byte[] buffer = new byte[2048];
                            int length = 0;
                            while ((length = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                newFileStream.Write(buffer, 0, length);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return string.Empty;
            }

            return newFileName;
        }
    }
}