using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Data;

namespace WebApp.Core.EntityClass
{
    public class AuthEntity
    {
        private readonly ApiDbContext DbContext;
        public AuthEntity(ApiDbContext dbContext)
        {
            DbContext = dbContext;
        }
        internal bool AccessAuthentication(string token)
        {
            var value = DbContext.SessionRecords.FirstOrDefault(x => x.Token == Guid.Parse(token));
            if (value != null)
            {
                return true;
            }
            return false;
        }
    }
}
