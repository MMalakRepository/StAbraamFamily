using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Core.Interfaces;
using StAbraamFamily.Web.Entities.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ChurchServantsRepo : GenericRepository<ChurchServant>, IChurchServant
    {
        public ChurchServantsRepo(SaintAbraamEntities context) : base(context)
        {

        }
        public SaintAbraamEntities StEntities { get { return Context as SaintAbraamEntities; } }

        public IEnumerable<ChurchServant> GetServantsByServiceID(int ID)
        {
            List<ChurchServant> churchServants = new List<ChurchServant>();
            var servants = StEntities.ChurchServants.Include(x => x.ServantServices);
            foreach (var servant in servants)
            {
                if (servant.ServantServices.Any(x => x.ChurchServiceID == ID))
                    churchServants.Add(servant);
            }
            return churchServants;
        }
    }
}