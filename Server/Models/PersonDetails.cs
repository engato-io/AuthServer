using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class PersonDetails
    {
        public int PersonId { get; set; }
        public int GenreId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Person Person { get; set; }
    }
}
