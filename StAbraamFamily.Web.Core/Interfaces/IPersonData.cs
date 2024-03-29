﻿using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StAbraamFamily.Web.Core.Interfaces
{
    public interface IPersonData : IEntityRepository<Person>
    {
        IEnumerable<Person> GetActivePeople();
        IEnumerable<Person> GetPeopleData();
    }
}
