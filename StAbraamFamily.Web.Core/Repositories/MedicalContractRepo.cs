using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Core.Interfaces;
using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class MedicalContractRepo : GenericRepository<MedicalContract>,IMedicalContracts
    {
        public MedicalContractRepo(DbContext context) : base(context)
        {
        }
        public SaintAbraamEntities StEntities { get { return Context as SaintAbraamEntities; } }

        public IEnumerable<MedicalContract> GetMedicalCOntractDetails()
        {
            var medicalContracts = StEntities.MedicalContracts.Where(x => x.IsFinished == false && x.IsActive == true)
                                                              .Include(m => m.Clinic).Include(m => m.Hospital)
                                                              .Include(m => m.MedicalService);
            return medicalContracts;
        }
    }
}
