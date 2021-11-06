using StAbraamFamily.Web.Core.General;
using StAbraamFamily.Web.Entities.Domain;
using StAbraamFamily.Web.Core.Repositories;
using StAbraamFamily.Web.Core.Interfaces;

namespace StAbraamFamily.Web.Core.Repositories
{
    public class StAbraamUOW : IUnitOfWork
    {
        private readonly SaintAbraamEntities familyDB;
        private IEntityRepository<Clinic> ClinicRepos;
        private IEntityRepository<Father> FathersRepos;
        private IEntityRepository<Family> FamilyRepos; 
        private IEntityRepository<ShroukFamily> ShroukFamilyRepos;
        private IEntityRepository<ServiceType> ServiceTypeRepos;
        private IEntityRepository<Hospital> HospitalRepos;
        private IServiceAction ServiceRepos; 
        private IChildrenData ChildRepos; 
        private IEntityRepository<FamilyServant> FSRepos;
        private IEntityRepository<FamilyVisit> VisitsRepos;
        private IEntityRepository<Servant> ServantRepos;
        private IPersonData PeopleRepos;
        private IEntityRepository<EvaluationLevel> EvaluationRepo;
        private IEntityRepository<MedicalService> MedicalServiceRep;
        private IMedicalContracts MedicalContractRepo;
        private IEntityRepository<CovidReservation> CovidReservationsRepo;
        public StAbraamUOW(SaintAbraamEntities familyDB)
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
        public IChildrenData Children
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
        public IPersonData People
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

        public IEntityRepository<ShroukFamily> ShroukFamilies
        {
            get
            {
                return ShroukFamilyRepos = ShroukFamilyRepos ?? new ShroukFamilyRepository(familyDB);
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
        public IServiceAction ServiceActions
        {
            get
            {
                return ServiceRepos = ServiceRepos ?? new ServiceActionsRepos(familyDB);
            }
        }
        public IEntityRepository<EvaluationLevel> EvaluationLevels
        {
            get
            {
                return EvaluationRepo = EvaluationRepo ?? new EvaluationRepository(familyDB);
            }
        }
        public IEntityRepository<MedicalService> MedicalServices
        {
            get
            {
                return MedicalServiceRep = MedicalServiceRep ?? new MedicalServicesRepo(familyDB);
            }
        }

        public IEntityRepository<CovidReservation> HealthReservations
        {
            get
            {
                return CovidReservationsRepo = CovidReservationsRepo ?? new CovidReservationsRepos(familyDB);
            }
        }

        public IMedicalContracts MedicalContracts
        {
            get
            {
                return MedicalContractRepo = MedicalContractRepo ?? new MedicalContractRepo(familyDB);
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