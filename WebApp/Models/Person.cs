using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int StatusId { get; set; }

        public virtual PersonDetails PersonDetails { get; set; }
        public virtual Status Status { get; set; }
    }
}
