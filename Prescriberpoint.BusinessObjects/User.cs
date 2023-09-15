using System;

namespace Prescriberpoint.BusinessObjects
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstnane { get; set; }
        public string Lastnane { get; set; }
        public bool IsAdmin { get; set; }
    }
}
