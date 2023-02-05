using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelkiHakiki.Core.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public DateTime? ResetExpireDate { get; set; }
        public Guid? ResetGuid { get; set; }
        public string Status
        {
            get
            {
                if (InUse)
                {
                    return "Aktif";
                }
                return "Pasif";
            }
        }
        public bool InUse { get; set; } = false;

    }
}
