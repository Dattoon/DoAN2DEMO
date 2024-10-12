using WebHocTap.Data.Entites.Base;
using WebHocTap.Share.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.Entites
{
    [Table(NameTable.NewsTable)]
    public class News: BaseEntitty
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string pathImg { get; set; }
        public int? IdCategoryNew { get; set; }
        public CategoryNew categoryNew { get; set; }
    }
}
