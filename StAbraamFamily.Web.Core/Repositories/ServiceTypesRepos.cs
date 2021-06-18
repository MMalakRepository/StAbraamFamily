using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ServiceTypesRepos : GenericRepository<ServiceType>
    {
        public ServiceTypesRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}