using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace XuongMay.Models.Entity
{
    public class Chain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedTime { get; set; }

        public bool IsDeleted { get; set; }

        public int ManagerId { get; set; }

        public virtual Account? Account { get; set; } // Mark as nullable

        public virtual ICollection<Task>? Tasks { get; set; } // Mark as nullable
    }

}
