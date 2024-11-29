using WebHocTap.Data.Entites.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHocTap.Share.Const;

namespace WebHocTap.Data.Entites
{
    [Table(NameTable.ComentTable)]
    public class Comemt : BaseEntitty
    {
        public string Content { get; set; }

        public int? IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }

        public int IdLesson { get; set; }

        [ForeignKey("IdLesson")]
        public Lesson Lesson { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public DateTime? UpdateAt { get; set; }
    }
}
