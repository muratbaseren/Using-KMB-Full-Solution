using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MuhendisAsci.Web.Models.Abstract
{
    public abstract class SysTableBase
    {
        [DisplayName("Oluşturma Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }

        [DisplayName("Oluşturan"), ScaffoldColumn(false)]
        public string CreatedUser { get; set; }

        [DisplayName("Güncelleme Tarihi"), ScaffoldColumn(false)]
        public DateTime? ModifiedOn { get; set; }

        [DisplayName("Güncelleyen"), ScaffoldColumn(false)]
        public string ModifiedUser { get; set; }
    }
}
