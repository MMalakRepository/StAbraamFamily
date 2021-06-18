using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StAbraamFamily.Web.Core.Interfaces
{
    public interface IServiceAction : IEntityRepository<ServiceAction>
    {
        IEnumerable<ServiceAction> GetOtherServiceDetails();
        IEnumerable<ServiceAction> GetAllMEdicalService();
        IEnumerable<ServiceAction> GetBagServices();
        IEnumerable<ServiceAction> GetFinancialServices();
        IEnumerable<ServiceAction> GetAllServiceDetails();

    }
}
