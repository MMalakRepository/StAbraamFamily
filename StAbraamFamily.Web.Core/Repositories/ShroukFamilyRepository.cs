using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ShroukFamilyRepository : GenericRepository<ShroukFamily>
    {
        public ShroukFamilyRepository(SaintAbraamEntities context) : base(context)
        {
        }
    }
}
