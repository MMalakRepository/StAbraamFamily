using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ServantsRepos : GenericRepository<Servant>
    {
        public ServantsRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}