using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data
{
    public class Commit
    {
        public Commit()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; init; } 

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public User Creator { get; set; }
        
        [Required]
        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}