using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuhendisAsci.Web.Models
{
    public class SysSocialItem
    {
        public Guid Id { get; set; }

        [DisplayName("Simge Adı"), Required, StringLength(30, ErrorMessage = "Max. 30 karakter.")]
        public string IconName { get; set; }

        [DisplayName("Bilgi"), Required, StringLength(30, ErrorMessage = "Max. 30 karakter.")]
        public string Title { get; set; }

        [DisplayName("Web Adresi"), Required, StringLength(255, ErrorMessage = "Max. 255 karakter.")]
        public string Url { get; set; }
    }
}
