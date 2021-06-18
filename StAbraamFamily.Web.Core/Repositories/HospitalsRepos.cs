using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class HospitalsRepos : GenericRepository<Hospital>
    {
        public HospitalsRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}