using Microsoft.AspNetCore.Mvc;

namespace XuongMay.Models
{
    public class AccountDTO
    {
        public string UserName { get; set; }
        public int Role { get; set; }
        public bool Status { get; set; }
    }

}
