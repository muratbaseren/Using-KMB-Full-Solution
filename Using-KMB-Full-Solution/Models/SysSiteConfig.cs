using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MuhendisAsci.Web.Models
{
    public class SysSiteConfig
    {
        private static string FileUri = HttpContext.Current.Server.MapPath("~/sys/bin/cfg.json");
        public static SysSiteConfig Obj { get; private set; } = new SysSiteConfig();

        public SysSiteConfig()
        {
            SocialItems = new List<SysSocialItem>();
        }

        [DisplayName("Sol Alan İçerik"), StringLength(300, ErrorMessage = "Max. 300 karakter.")]
        public string District1 { get; set; }

        [DisplayName("Sol Alan Başlık"), StringLength(30, ErrorMessage = "Max. 30 karakter.")]
        public string District1Title { get; set; }

        [DisplayName("Orta Alan Başlık"), StringLength(30, ErrorMessage = "Max. 30 karakter.")]
        public string District2Title { get; set; }

        [DisplayName("Sağ Alan İçerik"), StringLength(1000, ErrorMessage = "Max. 1000 karakter.")]
        public string District3 { get; set; }

        [DisplayName("Sağ Alan Başlık"), StringLength(30, ErrorMessage = "Max. 30 karakter.")]
        public string District3Title { get; set; }

        public List<SysSocialItem> SocialItems { get; set; }

        [DisplayName("Site Başlık"),Required(ErrorMessage ="Site Başlık alanı boş geçilemez."), StringLength(40, ErrorMessage = "Max. 40 karakter.")]
        public string Title { get; set; }

        [DisplayName("Site Açıklama"), StringLength(300, ErrorMessage = "Max. 300 karakter.")]
        public string Description { get; set; }

        [DisplayName("Site Yazar"), StringLength(150, ErrorMessage = "Max. 150 karakter.")]
        public string Author { get; set; }

        [DisplayName("Yazar Site Url"), StringLength(255, ErrorMessage = "Max. 255 karakter.")]
        public string AuthorUrl { get; set; }

        [DisplayName("Site Anahtar Kelimeler"), StringLength(500, ErrorMessage = "Max. 500 karakter.")]
        public string Keywords { get; set; }

        [DisplayName("İletişim Hata Mesajı"), StringLength(160, ErrorMessage = "Max. 160 karakter.")]
        public string ContactErrorMessage { get; set; }

        [DisplayName("İletişim Başarılı Mesajı"), StringLength(160, ErrorMessage = "Max. 160 karakter.")]
        public string ContactSuccessMessage { get; set; }

        [DisplayName("Siparişiniz Alındı Başlık"), StringLength(160, ErrorMessage = "Max. 160 karakter.")]
        public string OrderReceiveMessageTitle { get; set; }

        [DisplayName("Siparişiniz Alındı Mesajı"), StringLength(500, ErrorMessage = "Max. 500 karakter.")]
        public string OrderReceiveMessage { get; set; }

        [DisplayName("Siparişiniz Durumu Değişti Mesajı"), StringLength(500, ErrorMessage = "Max. 500 karakter.")]
        public string OrderStatusChangedMessage { get; set; }

        [DisplayName("Siparişiniz Durumu Değişti Başlık"), StringLength(160, ErrorMessage = "Max. 160 karakter.")]
        public string OrderStatusChangedMessageTitle { get; set; }


        public static SysSiteConfig LoadConfig()
        {
            if (HttpContext.Current.Cache["config"] != null)
            {
                Obj = HttpContext.Current.Cache["config"] as SysSiteConfig;
                return Obj;
            }

            if (System.IO.File.Exists(FileUri))
            {
                string json = System.IO.File.ReadAllText(FileUri);
                Obj = Newtonsoft.Json.JsonConvert.DeserializeObject<SysSiteConfig>(json);

                HttpContext.Current.Cache.Add("config", Obj, null, DateTime.Now.AddDays(7), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return Obj;
        }

        public static void SaveConfig()
        {
            System.IO.Directory.CreateDirectory(
                HttpContext.Current.Server.MapPath("~/sys/bin/"));

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Obj);
            System.IO.File.WriteAllText(FileUri, json);

            HttpContext.Current.Cache.Add("config", Obj, null, DateTime.Now.AddDays(7), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
        }
    }
}


