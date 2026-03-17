using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookVerse.DataModels
{
    public class UserBook
    {
        [Required]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
    }
}
