using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MinLength(2)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}   