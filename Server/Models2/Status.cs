using System;
using System.Collections.Generic;

namespace Server.Models2
{
    public partial class Status
    {
        public Status()
        {
            Genre = new HashSet<Genre>();
        }

        public int StatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Genre> Genre { get; set; }
    }
}
