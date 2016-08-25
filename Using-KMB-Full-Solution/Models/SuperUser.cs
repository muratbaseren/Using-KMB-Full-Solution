using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuhendisAsci.Web.Models
{
    public partial class SuperUser : DbTableBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "Kullanıcı adı boş geçilemez."), StringLength(20, ErrorMessage = "Max. 20 karalter.")]
        public string Username { get; set; }

        [DisplayName("E-Posta"), Required(ErrorMessage = "E-Posta boş geçilemez."), StringLength(150, ErrorMessage = "Max. 150 karalter.")]
        public string EMail { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "Şifre boş geçilemez."), StringLength(20, ErrorMessage = "Max. 20 karalter.")]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public bool IsDefault { get; set; }

        [DisplayName("Aktif Mi")]
        public bool IsActive { get; set; }
    }
}
