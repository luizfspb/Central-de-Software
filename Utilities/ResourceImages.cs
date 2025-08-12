using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace Central_de_Software.Utilities
{
    public static class ResourceImages
    {
        public static Image GetImage(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // Tenta .png primeiro, depois .ico
            string[] tryNames = {
                $"Central_de_Software.Resources.{resourceName.Replace(".ico", ".png")}",
                $"Central_de_Software.Resources.{resourceName}"
            };
            foreach (var name in tryNames)
            {
                using (var stream = assembly.GetManifestResourceStream(name))
                {
                    if (stream != null)
                        return Image.FromStream(stream);
                }
            }
            return null;
        }
    }
    public static class TechModeConfig
    {
        private static readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config_techmode.dat");
        private static readonly string defaultPassword = "1212";

        public static string GetPassword()
        {
            if (!File.Exists(configPath))
            {
                SetPassword(defaultPassword);
                return defaultPassword;
            }
            try
            {
                var base64 = File.ReadAllText(configPath);
                var bytes = Convert.FromBase64String(base64);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return defaultPassword;
            }
        }

        public static void SetPassword(string newPassword)
        {
            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var base64 = Convert.ToBase64String(bytes);
            File.WriteAllText(configPath, base64);
        }
    }
}