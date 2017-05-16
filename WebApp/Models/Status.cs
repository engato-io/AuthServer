using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Status
    {
        public Status()
        {
            Genre = new HashSet<Genre>();
            Person = new HashSet<Person>();
        }

        public int StatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Genre> Genre { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
