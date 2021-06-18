using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class FamilyVisitsRepos : GenericRepository<FamilyVisit>
    {
        public FamilyVisitsRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}