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
    [Table(NameTable.MstPerMissionTable)]
    public class MstPerMission :BaseEntitty
    {
        public MstPerMission()
        {
            rolePermissions = new HashSet<RolePermission>();
        }

        public string Code { get; set; }
        public string Table { get; set; }
        public string Desc { get; set; }
        public string GroupName { get; set; }

        public ICollection<RolePermission> rolePermissions { get; set; }
    }
}
