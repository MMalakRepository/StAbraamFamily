using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class MedicalServicesRepo : GenericRepository<MedicalService>
    {
        public MedicalServicesRepo(SaintAbraamEntities context) : base(context)
        {
        }
    }
}
