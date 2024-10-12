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
    [Table(NameTable.ComentTable)]
    public class Comemt: BaseEntitty
    {
        public string Content { get; set; }
        public int? IdUser { get; set; }
        public int IdLesson { get; set; }
        public Lesson lesson { get; set; }
    }
}
