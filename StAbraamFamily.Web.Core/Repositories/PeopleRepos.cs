using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Core.Interfaces;
using StAbraamFamily.Web.Entities.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class PeopleRepos : GenericRepository<Person>,IPersonData
    {
        public PeopleRepos(SaintAbraamEntities context) : base(context)
        {
        }
        public SaintAbraamEntities StEntities { get { return Context as SaintAbraamEntities; } }
        public IEnumerable<Person> GetActivePeople()
        {
            var People = StEntities.People.Where(x => x.IsActive == true).ToList();
            return People;
        }

        public IEnumerable<Person> GetPeopleData()
        {
            var people = StEntities.People.Where(x => x.IsActive == true).Include(p => p.Father).Include(p => p.Servant).ToList();
            return people;
        }
    }
}