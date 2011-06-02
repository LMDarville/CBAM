
using System;
using System.Linq;
using System.Collections.Generic;

namespace CBAM.Models
{

    public interface IArchitecturalStrategyRepository
    {

        ArchitecturalStrategy GetByID(int id);
        IQueryable<ArchitecturalStrategy> GetAll();
        IQueryable<ArchitecturalStrategy> GetAllbyProjID(long projID);
        IQueryable<Scenario> GetAllScenarios();
        IQueryable<Scenario> GetAffiliatedScenariosByStratID(long stratID);
        Scenario GetScenarioByID(int id);
        ExpectedUtility GetExpectedUtilityByID(long utilID);

        void Add(ArchitecturalStrategy strategy);
        void Delete(ArchitecturalStrategy strategy);
        void UpdateIsComplete(ArchitecturalStrategy strategy);
        void UpdateAffectedScenarios(long strategyID, int[] scenarioIDs);
        void ClearAllScenariosFromStrategy(long strategyID);


        void Save();
    }

    
}

