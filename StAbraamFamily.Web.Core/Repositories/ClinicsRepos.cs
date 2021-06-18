using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ClinicsRepos : GenericRepository<Clinic>
    {
        public ClinicsRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}