using Microsoft.AspNetCore.Mvc;

namespace XuongMay.Models
{
    public class AccountDTO : Controller
    {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public int Role { get; set; }
            public bool Status { get; set; }

    }
}
