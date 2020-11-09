using StAbraamFamily.Models;
using StAbraamFamily.Repositories.GeneralRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StAbraamFamily.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEntityRepository<Clinic> Clinics { get; }
        IEntityRepository<ServiceType> ServiceTypes { get; }
        IEntityRepository<Hospital> Hospitals { get; }
        IEntityRepository<Child> Children { get; }
        IEntityRepository<FamilyVisit> FamilyVisits { get; }
        IEntityRepository<Person> People { get; }
        IEntityRepository<Family> Families { get; }
        IEntityRepository<Father> Fathers { get; }
        IEntityRepository<Servant> Servants { get; }
        IEntityRepository<FamilyServant> FamilyServants { get; }
        IEntityRepository<ServiceAction> ServiceActions { get; }
        int Complete();
    }
}
