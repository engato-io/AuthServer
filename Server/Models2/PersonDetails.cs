using System;
using System.Collections.Generic;

namespace Server.Models2
{
    public partial class PersonDetails
    {
        public int PersonId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual Person Person { get; set; }
    }
}
