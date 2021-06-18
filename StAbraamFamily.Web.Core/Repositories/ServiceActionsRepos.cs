using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Core.Interfaces;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ServiceActionsRepos : GenericRepository<ServiceAction>, IServiceAction
    {
        public ServiceActionsRepos(SaintAbraamEntities context) : base(context)
        {

        }

        public SaintAbraamEntities StEntities { get { return Context as SaintAbraamEntities; } }

        public IEnumerable<ServiceAction> GetAllMEdicalService()
        {
            var services = StEntities.ServiceActions.Include(s => s.AspNetUser)
                                    .Include(s => s.Clinic).Include(s => s.Family)
                                    .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                                    .Include(s => s.ServiceType).Where(x => x.IsActive == true && x.ActionTypeID == 2);
            return services;
        }

        public IEnumerable<ServiceAction> GetOtherServiceDetails()
        {
            var services = StEntities.ServiceActions.Include(s => s.AspNetUser)
                                        .Include(s => s.Clinic).Include(s => s.Family)
                                        .Include(s => s.Hospital).Include(s => s.Person)
                                        .Include(s => s.Servant).Include(s => s.ServiceType)
                                        .Where(x => x.IsActive == true && (x.ServiceType.ID != 1 || x.ServiceType.ID != 2)).ToList();
            return services;
        }

        public IEnumerable<ServiceAction> GetBagServices()
        {
            var serviceActions = StEntities.ServiceActions.Include(s => s.AspNetUser)
               .Include(s => s.Clinic).Include(s => s.Family)
               .Include(s => s.Hospital).Include(s => s.Person)
               .Include(s => s.Servant).Include(s => s.ServiceType)
               .Where(x => x.IsActive == true && x.ActionTypeID == 1);
            return serviceActions;
        }

        public IEnumerable<ServiceAction> GetFinancialServices()
        {
           var services = StEntities.ServiceActions.Include(s => s.AspNetUser)
                                    .Include(s => s.Clinic).Include(s => s.Family)
                                    .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                                    .Include(s => s.ServiceType).Where(x => x.IsActive == true && x.ServiceType.ID == 3);
            return services;
        }

        public IEnumerable<ServiceAction> GetAllServiceDetails()
        {
           var services = StEntities.ServiceActions.Include(s => s.AspNetUser)
                        .Include(s => s.Clinic).Include(s => s.Family)
                        .Include(s => s.Hospital).Include(s => s.Person).Include(s => s.Servant)
                        .Include(s => s.ServiceType).Where(x => x.IsActive == true);
            return services;
        }
    }

}