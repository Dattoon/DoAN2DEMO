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
    [Table(NameTable.AnswerTable)]
    public class Answer:BaseEntitty
    {
        public string Description { get; set; }
        public bool IsSucces { get; set; }
        public int? IdQues { get; set; }
        public Test test { get; set; }
    }
}
