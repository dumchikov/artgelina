using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Serialization;

namespace artgelina.web.Models
{
    [Serializable]
    public class ArtgelinaModel
    {
        [Display(Name = "Header image link")]
        public string HeaderImage { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        [Display(Name = "Portfolio text")]
        public string PortfolioText { get; set; }

        [XmlIgnore]
        public IEnumerable<dynamic> Images { get; set; }

        [Display(Name = "About me image")]
        public string AboutMeImage { get; set; }

        [Display(Name = "About me text")]
        public string AboutMeText { get; set; }

        [Display(Name = "Contact text")]
        public string ContactText { get; set; }

        public static string GetBase64(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}