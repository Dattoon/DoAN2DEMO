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
    [Table(NameTable.SubjectTable)]
    public class Subject:BaseEntitty
    {
        public Subject() { 
            chapters=new HashSet<Chapter>();
        }
        public string NameSubject { get; set; }
        public int? IdCategorySub { get; set; }
        public CategorySub categorySub { get; set; }
        public ICollection<Chapter> chapters { get; set; }
    }
}
