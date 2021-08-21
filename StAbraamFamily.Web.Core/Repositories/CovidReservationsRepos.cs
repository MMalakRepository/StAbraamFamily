using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class CovidReservationsRepos : GenericRepository<CovidReservation>
    {
        public CovidReservationsRepos(SaintAbraamEntities context) : base(context)
        {
        }
    }
}
