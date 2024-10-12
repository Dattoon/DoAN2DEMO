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
    [Table(NameTable.CategorySubTable)]
    public class CategorySub:BaseEntitty
    {
        public CategorySub() { 
            subjects=new HashSet<Subject>();
            purchasedCourses=new HashSet<PurchasedCourse>();
        }
        public string NameCategorySub { get; set; }
        public int? Price { get; set; }
        public string pathImg { get; set; }
        public int CountView { get; set; }
        public string Descripstion { get; set; }
        public ICollection<Subject> subjects { get; set; }
        public ICollection<PurchasedCourse> purchasedCourses { get; set; }
    }
}
