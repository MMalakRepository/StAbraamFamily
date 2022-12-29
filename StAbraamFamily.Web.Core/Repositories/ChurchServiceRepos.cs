using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ChurchServiceRepos : GenericRepository<ChurchService>
    {
        public ChurchServiceRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}
