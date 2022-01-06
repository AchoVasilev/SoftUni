using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data
{
    public class User
    {
        public User()
        {
            this.Repositories = new List<Repository>();
            this.Commits = new List<Commit>();
        }

        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [MaxLength(20)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Repository> Repositories { get; set; }

        public ICollection<Commit> Commits { get; set; }
    }
}
