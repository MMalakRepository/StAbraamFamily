using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class FamilyRepository : GenericRepository<Family>
    {
        public FamilyRepository(SaintAbraamEntities context) : base(context)
        {
        }
    }
}
