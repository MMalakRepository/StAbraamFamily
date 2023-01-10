using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Core.Interfaces;
using StAbraamFamily.Web.Entities.Domain;
using System;

namespace StAbraamFamily.Web.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEntityRepository<Clinic> Clinics { get; }
        IEntityRepository<ServiceType> ServiceTypes { get; }
        IEntityRepository<Hospital> Hospitals { get; }
        IChildrenData Children { get; }
        IEntityRepository<FamilyVisit> FamilyVisits { get; }
        IPersonData People { get; }
        IEntityRepository<Family> Families { get; }
        IEntityRepository<Father> Fathers { get; }
        IEntityRepository<Servant> Servants { get; }
        IEntityRepository<FamilyServant> FamilyServants { get; }
        IServiceAction ServiceActions { get; }
        IEntityRepository<EvaluationLevel> EvaluationLevels { get; }
        IEntityRepository<MedicalService> MedicalServices { get; }
        IMedicalContracts MedicalContracts { get; }
        IEntityRepository<CovidReservation> HealthReservations { get; }
        IChurchServant ChurchServants { get; }
        IEntityRepository<ChurchService> ChurchServices { get; }
        int Complete();
    }
}
