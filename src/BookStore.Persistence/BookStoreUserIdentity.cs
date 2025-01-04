using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Persistence
{
    public class BookStoreUserIdentity : IdentityUser
    {
        [StringLength(50)]
        public string FirtName { get; set; } = default!;
        [StringLength(50)]
        public string LastName { get; set; } = default!;
        public string Position { get; set; } = default!;
        public DocumentTypeEnum DocumentType { get; set; }
        public string EmployeeNumber { get; set; } = default!;
    }
    public enum DocumentTypeEnum : short
    {
        employeenumber
    }
}
