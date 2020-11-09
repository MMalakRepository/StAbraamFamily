using StAbraamFamily.Models;
using StAbraamFamily.Repositories.GeneralRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StAbraamFamily.Repositories.ServiceRepositories
{
    public class FamilyServantsRepos : GenericRepository<FamilyServant>
    {
        public FamilyServantsRepos(StAbraamEntities context) : base(context)
        {
        }
    }
}