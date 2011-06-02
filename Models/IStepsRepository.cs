
using System;
using System.Linq;
using System.Collections.Generic;

namespace CBAM.Models
{

    public interface IStepsRepository
    {
        IQueryable<Scenario> GetTopThird(long projectID);
        IQueryable<Scenario> GetTopSixth(long projectID);
        IQueryable<Scenario> GetBottomTwoTopThirds(long projectID);
        IQueryable<Step> GetAll();
        Step GetByStepID(int id);
        Step GetNextStepToComplete(long projectID);

        bool Step01HasOneScenario(long projectID); //(1) At least one scenario
        //(2) Top third Quality Attributes Responses are not null
        bool Step02TopThirdQualityAttributesResponsesDefined(long projectID);
        bool Step03TopThirdVotesSumTo100(long projectID);     //(3) Edit Votes
        bool Step04TopSixthUtilitiesDefined(long projectID);  //(4) Edit Utility
    

        IQueryable<Step> Update();
        void Save();
  }

    
}

