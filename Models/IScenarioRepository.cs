
using System;
using System.Linq;
using System.Collections.Generic;

namespace CBAM.Models
{

    public interface IScenarioRepository
    {

        //IQueryable<Scenario> GetAll();
        IQueryable<Scenario> GetByProjectID(long projectID);
        Scenario GetByID(int id);
        Project GetProjectByID(long id);
        IQueryable<long> GetUtilitiesByScenarioID(int id);
        Utility GetUtilityByID(int id);

        IQueryable<Utility> GetTopThirdUtilities(long projectID);
        IQueryable<Scenario> GetTopThird(long projectID);
        IQueryable<Scenario> GetTopSixth(long projectID);
        List<Scenario> GetTopThirdList(long projectID);
        IQueryable<Scenario> GetBottomTwoTopThirds(long projectID);
        IEnumerable<Importance> ImportanceList();
        IQueryable <Step> Steps();
        Step NextStepToComplete();

        IQueryable<Utility> UtilityList(long projectID);
        IQueryable<Scenario> ScenarioList(long projectID);
        
     
        void Add(Scenario Scenario);
        void addScenarioToUtility(long secnarioID);
        void ClearVotesBottomTwoThirds(long projectID);
        void Delete(Scenario Scenario);

        void Save();
    }

    
}

