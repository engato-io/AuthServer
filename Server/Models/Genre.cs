using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Genre
    {
        public Genre()
        {
            PersonDetails = new HashSet<PersonDetails>();
        }

        public int GenreId { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }

        public virtual ICollection<PersonDetails> PersonDetails { get; set; }
        public virtual Status Status { get; set; }
    }
}
