using StAbraamFamily.Models;
using StAbraamFamily.Repositories.GeneralRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StAbraamFamily.Repositories.ServiceRepositories
{
    public class PeopleRepos : GenericRepository<Person>
    {
        public PeopleRepos(StAbraamEntities context) : base(context)
        {
        }
    }
}