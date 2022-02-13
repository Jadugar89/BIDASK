using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BIDASK.Shared
{
    public class UserProfil
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string XTB_name { get; set; }
        public string XTB_pass { get; set; }
    }
}
