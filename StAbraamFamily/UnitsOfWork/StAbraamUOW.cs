using StAbraamFamily.Models;
using StAbraamFamily.Repositories.GeneralRepositories;
using StAbraamFamily.Repositories.ServiceRepositories;
using StAbraamFamily.Services.ServiceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StAbraamFamily.UnitsOfWork
{
    public class StAbraamUOW : IUnitOfWork
    {
        private readonly StAbraamEntities familyDB;
        private IEntityRepository<Clinic> ClinicRepos;
        private IEntityRepository<Father> FathersRepos;
        private IEntityRepository<Family> FamilyRepos; 
        private IEntityRepository<ServiceType> ServiceTypeRepos;
        private IEntityRepository<Hospital> HospitalRepos;
        private IEntityRepository<ServiceAction> ServiceRepos; 
        private IEntityRepository<Child> ChildRepos; 
        private IEntityRepository<FamilyServant> FSRepos;
        private IEntityRepository<FamilyVisit> VisitsRepos;
        private IEntityRepository<Servant> ServantRepos;
        private IEntityRepository<Person> PeopleRepos;
        public StAbraamUOW(StAbraamEntities familyDB)
        {
            this.familyDB = familyDB;
        }
        public IEntityRepository<Clinic> Clinics
        {
            get{
                return ClinicRepos = ClinicRepos ?? new ClinicsRepos(familyDB);
            }
        }

        public IEntityRepository<ServiceType> ServiceTypes
        {
            get
            {
                return ServiceTypeRepos = ServiceTypeRepos ?? new ServiceTypesRepos(familyDB);
            }
        }

        public IEntityRepository<Hospital> Hospitals
        {
            get
            {
                return HospitalRepos = HospitalRepos ?? new HospitalsRepos(familyDB);
            }
        }

        public IEntityRepository<Child> Children
        {
            get
            {
                return ChildRepos = ChildRepos ?? new ChildrenRepos(familyDB);
            }
        }

        public IEntityRepository<FamilyVisit> FamilyVisits
        {
            get
            {
                return VisitsRepos = VisitsRepos ?? new FamilyVisitsRepos(familyDB);
            }
        }

        public IEntityRepository<Person> People
        {
            get
            {
                return PeopleRepos = PeopleRepos ?? new PeopleRepos(familyDB);
            }
        }

        public IEntityRepository<Family> Families
        {
            get
            {
                return FamilyRepos = FamilyRepos ?? new FamilyRepository(familyDB);
            }
        }

        public IEntityRepository<Father> Fathers
        {
            get
            {
                return FathersRepos = FathersRepos ?? new FathersRepos(familyDB);
            }
        }

        public IEntityRepository<Servant> Servants
        {
            get
            {
                return ServantRepos = ServantRepos ?? new ServantsRepos(familyDB);
            }
        }

        public IEntityRepository<FamilyServant> FamilyServants
        {
            get
            {
                return FSRepos = FSRepos ?? new FamilyServantsRepos(familyDB);
            }
        }

        public IEntityRepository<ServiceAction> ServiceActions
        {
            get
            {
                return ServiceRepos = ServiceRepos ?? new ServiceActionsRepos(familyDB);
            }
        }
        public int Complete()
        {
            return familyDB.SaveChanges();
        }

        public void Dispose()
        {
            familyDB.Dispose();
        }
    }
}