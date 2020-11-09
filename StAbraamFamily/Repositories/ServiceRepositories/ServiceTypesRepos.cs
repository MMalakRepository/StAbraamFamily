using StAbraamFamily.Models;
using StAbraamFamily.Repositories.GeneralRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StAbraamFamily.Repositories.ServiceRepositories
{
    public class ServiceTypesRepos : GenericRepository<ServiceType>
    {
        public ServiceTypesRepos(StAbraamEntities context) : base(context)
        {
        }
    }
}