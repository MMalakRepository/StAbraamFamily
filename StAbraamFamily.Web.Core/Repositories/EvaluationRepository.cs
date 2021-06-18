using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class EvaluationRepository : GenericRepository<EvaluationLevel>
    {
        public EvaluationRepository(SaintAbraamEntities context) : base(context)
        {
        }
    }
}