using SqLib.SqLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallerie.Data
{
    public class UserRepository : SqLiteBaseRepository<User, int>
    {
        private static readonly object _lockCreate = new object();
        public UserRepository() : base("User", new string[] { "Id" }, "Id", Config.DbFilePath, true)
        {

        }
        public override void Create(User value)
        {
            lock(_lockCreate)
            { 
                base.Create(value);
                int newId = QueryAny<int>("select max(" + _idFieldName + ") from " + _tableName).First();
                value.Id = newId;
            }
        }
    }
}
