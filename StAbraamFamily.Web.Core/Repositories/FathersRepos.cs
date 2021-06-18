using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class FathersRepos : GenericRepository<Father>
    {
        public FathersRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}