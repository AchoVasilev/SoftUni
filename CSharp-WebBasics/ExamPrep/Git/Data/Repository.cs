using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data
{
    public class Repository
    {
        public Repository()
        {
            this.Commits = new List<Commit>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; init; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }

        [Required]
        public User Owner { get; set; }

        public ICollection<Commit> Commits { get; set; }
    }
}