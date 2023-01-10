using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;
using System.Collections.Generic;

namespace StAbraamFamily.Web.Core.Interfaces
{
    public interface IChurchServant : IEntityRepository<ChurchServant>
    {
        IEnumerable<ChurchServant> GetServantsByServiceID(int ID);

    }
}
