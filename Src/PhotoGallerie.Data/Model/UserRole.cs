using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlLib;

namespace PhotoGallerie.Data
{
    public class UserRole: DomainObjectBase<int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
