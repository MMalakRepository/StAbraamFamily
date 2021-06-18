using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class FamilyServantsRepos : GenericRepository<FamilyServant>
    {
        public FamilyServantsRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}