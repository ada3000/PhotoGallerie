using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqLib.SqLite;
using SqlLib;

namespace PhotoGallerie.Data
{
    public class SimpleRepository<T> : SqLiteBaseRepository<T, int> where T : DomainObjectBase<int>
    {
        public SimpleRepository() : base(Config.DbFilePath)
        {

        }
    }
}
