using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StAbraamFamily.Web.Core.Interfaces
{
    public interface IChildrenData : IEntityRepository<Child>
    {
        IEnumerable<Child> GetChildrenData();
        IEnumerable<Child> GetChildrenDataByFamilyID(int familyID);
    }
}
