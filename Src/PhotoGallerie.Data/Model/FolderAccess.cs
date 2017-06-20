using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlLib;

namespace PhotoGallerie.Data
{
    public class FolderAccess : DomainObjectBase<int>
    {
        public string FolderPath { get; set; }
        public int RoleId { get; set; }
    }
}
