using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace EnclaveDrive.Utils
{
    internal class ThemeService
    {
        public class Theme
        {
            public string PrimaryColor { get; set; }
            public string SecondaryColor { get; set; }
            public string BackgroundColor { get; set; }
            public string PanelColor { get; set; }
            public string TextColor { get; set; }
            public string BorderColor { get; set; }
        }

        private Dictionary<string, Dictionary<string, Theme>> themes;

        public ThemeService()
        {
            LoadThemes();
        }

        public void LoadThemes()
        {
            string themeFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Themes.json");

            if (File.Exists(themeFilePath))
            {
                var json = File.ReadAllText(themeFilePath);
                themes = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Theme>>>(json);
            }
            else
            {
                throw new FileNotFoundException("El archivo Themes.json no fue encontrado.");
            }
        }

        public Theme GetTheme(string mode, string variant)
        {
            if (themes.ContainsKey(mode) && themes[mode].ContainsKey(variant))
            {
                return themes[mode][variant];
            }

            throw new ArgumentException($"El tema '{mode}/{variant}' no existe.");
        }
    }
}
