using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace XuongMay.Models.Entity
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string UserPassword { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public bool Status { get; set; }

        public ICollection<Chain> Chains { get; set; }
    }
}
