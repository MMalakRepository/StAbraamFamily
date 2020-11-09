using StAbraamFamily.Models;
using StAbraamFamily.Repositories.GeneralRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StAbraamFamily.Repositories.ServiceRepositories
{
    public class ChildrenRepos : GenericRepository<Child>
    {
        public ChildrenRepos(StAbraamEntities context) : base(context)
        {
        }
    }
}