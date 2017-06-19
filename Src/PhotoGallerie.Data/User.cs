using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlLib;

namespace PhotoGallerie.Data
{
    public class User: DomainObjectBase<int>
    {
        public string Name { get; set; }
    }
}
