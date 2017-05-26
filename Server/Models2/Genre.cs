using System;
using System.Collections.Generic;

namespace Server.Models2
{
    public partial class Genre
    {
        public Genre()
        {
            Person = new HashSet<Person>();
        }

        public int GenreId { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }

        public virtual ICollection<Person> Person { get; set; }
        public virtual Status Status { get; set; }
    }
}
