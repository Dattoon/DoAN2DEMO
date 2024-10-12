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
    [Table(NameTable.LessonTable)]
    public class Lesson :BaseEntitty
    {
        public Lesson() { 
            comemts=new HashSet<Comemt>();
        }
        public string Content { get; set; }
        public string? Video { get; set; }
        public int? IdChapter { get; set; }
        public Chapter chapter { get; set; }
        public ICollection<Comemt> comemts { get; set; }
    }
}
