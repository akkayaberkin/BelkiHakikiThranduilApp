using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelkiHakiki.Core.DTOs
{
    public class UserUpdateDto
    {
        public string PhotoUrl { get; set; }
        public DateTime UpdatedDate { get; set; }
       
        public bool InUse { get; set; } = true;
    }
}
