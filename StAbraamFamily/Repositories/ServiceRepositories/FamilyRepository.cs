using StAbraamFamily.Models;
using StAbraamFamily.Repositories.GeneralRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StAbraamFamily.Services.ServiceRepositories
{
    public class FamilyRepository : GenericRepository<Family>
    {
        public FamilyRepository(StAbraamEntities context) : base(context)
        {
        }
    }
}
