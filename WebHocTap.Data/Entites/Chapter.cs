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
    [Table(NameTable.ChapterTable)]
    public class Chapter : BaseEntitty
    {
        public Chapter() { 
            lessons=new HashSet<Lesson>();
            tests=new HashSet<Test>();
        }
        public string NameChapter { get; set; }
        public int? IdSubject { get; set; }
        public Subject subject { get; set; }
        public ICollection<Test> tests { get; set; }
        public ICollection<Lesson> lessons { get; set; }
    }
}
