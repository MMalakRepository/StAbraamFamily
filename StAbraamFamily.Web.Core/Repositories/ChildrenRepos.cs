using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Core.Interfaces;
using StAbraamFamily.Web.Entities.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class ChildrenRepos : GenericRepository<Child>,IChildrenData
    {
        public ChildrenRepos(SaintAbraamEntities context) : base(context)
        {
        }

        public SaintAbraamEntities StEntities { get { return Context as SaintAbraamEntities; } }
        public IEnumerable<Child> GetChildrenData()
        {
            var children = StEntities.Children.Where(x => x.IsActive == true).Include(c => c.Family).Include(c => c.Father).Include(c => c.Servant);
            return children;
        }

        public IEnumerable<Child> GetChildrenDataByFamilyID(int familyID)
        {
            var children = StEntities.Children.Where(x => x.IsActive == true && x.FamilyID == familyID).Include(c => c.Family).Include(c => c.Father).Include(c => c.Servant);
            return children;
        }
    }
}