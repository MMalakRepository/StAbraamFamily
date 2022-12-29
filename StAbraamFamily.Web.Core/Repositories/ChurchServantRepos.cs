using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ChurchServantRepos : GenericRepository<ChurchServant>
    {
        public ChurchServantRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}
