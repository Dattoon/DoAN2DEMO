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
    [Table(NameTable.ReportTable)]
    public class Report:BaseEntitty
    {
        public string Content { get; set; }
        public int? IdUser { get; set; }
    }
}
