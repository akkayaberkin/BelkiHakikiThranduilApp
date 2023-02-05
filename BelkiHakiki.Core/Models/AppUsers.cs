using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelkiHakiki.Core
{
    public class AppUsers:BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AppRole { get; set; }
        public string Email{ get; set; }
        //public bool Gender{ get; set; }
        public bool InUse { get; set; } = true;
        public DateTime? ResetExpireDate { get; set; }
        public Guid? ResetGuid { get; set; }
    }
}
