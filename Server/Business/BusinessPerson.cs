using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Server.Models;

namespace WebApp.Business
{
    public class BusinessPerson
    {
        readonly TESTContext _context;

        public BusinessPerson(TESTContext context)
        {
            _context = context;
        }

        public bool CheckPassword(Person User, string password)
        {
            return _context.PersonDetails.Count(x => x.PersonId == User.PersonId && x.Password == password) > 0 ? true : false;
        }

        public Person GetUser(ClaimsPrincipal claim)
        {
            return _context.Person.SingleOrDefault(x => x.PersonId == int.Parse(claim.FindFirst(n => n.Type == "sub").Value));
        }
    }
}
