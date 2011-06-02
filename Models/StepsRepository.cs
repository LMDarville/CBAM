using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBAM.Models
{
    public class StepsRepository : CBAM.Models.IStepsRepository
    {

        private CBAMDataContext db = new CBAMDataContext();


        //Query Methods
        public IQueryable<Step> GetAll()
        {
            return db.Steps;
        }


        public Step GetByStepID(int ID)
        {
            return db.Steps.SingleOrDefault(s => s.Step1 == ID);
        }

        //(1) At least one scenario:  Updated -- must have at least 3 scenarios
        public bool Step01HasOneScenario(long projectID)
        {   //returns true if at least 3 scenarios exist in the database
            return (db.Scenarios.Where(x=>x.ProjectID == projectID).FirstOrDefault() != null) &&
                    db.Scenarios.Where(x => x.ProjectID == projectID).Count() > 3;
        }

        //(2) Top third Quality Attributes Responses are not null
        public bool Step02TopThirdQualityAttributesResponsesDefined(long projectID)
        {   //returns true if Qual Attr Resp have been 
            //defined for top 1/3 scenarios
            var topthird = GetTopThirdUtilities(projectID);
            return !topthird.Any(x => x.Description == null)
                 && !topthird.Any(x => x.Description.Length < 1);
                 //if any are null, step is not complete
        }
        
        //(3) Edit Votes
        public bool Step03TopThirdVotesSumTo100(long projectID)
        {  //returns true if votes for top 1/3 scnearios = 100.
            var topthird = GetTopThird(projectID);
            var sum = topthird.Select(x => x.Votes).Sum();
            return (sum == 100);
        }

        //(4) Edit Utility
        public bool Step04TopSixthUtilitiesDefined(long projectID)
        {   //returns true if Utility has been 
            //defined for top 1/6 scenarios
            var top = GetTopSixthUtilities(projectID);
            return !top.Any(x => x.Utility1 == null);  //if any are null, step is not complete
                
        }

        //(5) Edit Strategy
        public bool Step05StrategiesDefined(long projectID)
        {   //for updating indiv sceanrio see "ArchitecturalStrategyReposity"
            //complete defined as each scenario has: 1) name, 2) description, 
            //3) at least one affected scenario
            //4) expected utility and exepected utility descrtiption for each affected scenario
            var stategies = db.ArchitecturalStrategies.Where(x => x.ProjectID == projectID);
      
            return (!stategies.Any(x => x.IsComplete == false) && (stategies.Count()>0));
            //!Any(.IsComplete) will be false if any are false.
            //At least one strategy must exist
            //Any and Count must both be nonzero to return true

            //bool complete = new bool();
            //if ( !stategies.Any(x => x.Description == null)
            //     && !stategies.Any(x => x.Name == null)
            //     && stategies.Select(x => x.ExpectedUtilities).Count() > 0
            //     && !stategies.Select(x => x.ExpectedUtilities)
            //            .Any(y => y.Select(z => z.ExpectedUtility1) == null)
            //     && !stategies.Select(x => x.ExpectedUtilities)
            //            .Any(y => y.Select(z => z.ExpectedUtilityDescription) == null)
            //     )
            //     { complete = true; }
            //else { complete = false; }
            //return complete;
        }
        //updates complete stat us on each steps and returns the next step to be completed
        public void UpdateSteps(long projectID)
        {   //update complete status

            //(1) At least one scenario
            GetByStepID(1).Complete =  Step01HasOneScenario(projectID);

            //(2) Top third Quality Attributes Response Goals are not null
            GetByStepID(2).Complete = Step02TopThirdQualityAttributesResponsesDefined(projectID);

            //(3) Edit Votes
            GetByStepID(3).Complete = Step03TopThirdVotesSumTo100(projectID);
            
            //(4) Edit Utility
            GetByStepID(4).Complete = Step04TopSixthUtilitiesDefined(projectID);

            //(5) Develop Architectural Strategies
            GetByStepID(5).Complete = Step05StrategiesDefined(projectID);
            
            Save();
            return;
        }


        public Step GetNextStepToComplete(long projectID)
        {
            UpdateSteps(projectID); //makes sure current
            Save();

            var incompleteSteps =
                from st in db.Steps
                orderby st.Step1
                where (st.Complete == false || st.Complete == null)
                select st;

            var nextStep = new Step();

            if (incompleteSteps.Count() == 0)
            {//all steps have been completed
                nextStep.Step1 = 99; //arbitrary number for last step
                nextStep.Description = "All Steps Complete!";

            }
            else
            {
                nextStep = incompleteSteps.First();
            }

            return nextStep;
        }


        //From ScenarioRepository--
        public IQueryable<Scenario> GetTopThird(long projectID)
        {
            //get max based on number of rows
            int x = (int)Math.Round((double)db.Scenarios.Where(s => s.ProjectID == projectID).Distinct().Count() / 3.0); //set priority marker at 1/3

            //var q = db.Scenarios.Select( (p, index) => new {Position = index, p.Priority}).Where(p => p.Position == x);
            //var q = db.Scenarios.Select(p => new { p.Priority }).OrderBy(p => p.Priority).GroupBy(p => p.Priority).ElementAt(x);

            // Create the query.
            IQueryable<Scenario> topThird =
                from scenario in db.Scenarios
                where scenario.Priority <= x && scenario.ProjectID == projectID
                orderby scenario.Priority
                select scenario;
            return topThird;
        }

        //From ScenarioRepository--
        public IQueryable<Scenario> GetTopSixth(long projectID)
        {
            //get max based on number of rows
            int x = (int)Math.Round((double)db.Scenarios.Where(s => s.ProjectID == projectID).Distinct().Count() / 6.0); //set priority marker at 1/3
     
            // Create the query.
            IQueryable<Scenario> top =
                from scenario in db.Scenarios
                where scenario.Priority <= x && scenario.ProjectID == projectID
                orderby scenario.Votes descending, scenario.Priority
                select scenario;
            return top;
        }

        public IQueryable<Scenario> GetBottomTwoTopThirds(long projectID)
        {  //get max based on number of rows
            int x = (int)Math.Round((double)db.Scenarios.Where(s => s.ProjectID == projectID).Distinct().Count() / 3.0); //set priority marker at 1/3

            //var q = db.Scenarios.Select( (p, index) => new {Position = index, p.Priority}).Where(p => p.Position == x);
            //var q = db.Scenarios.Select(p => new { p.Priority }).OrderBy(p => p.Priority).GroupBy(p => p.Priority).ElementAt(x);

            // Create the query.
            IQueryable<Scenario> topThird =
                from scenario in db.Scenarios
                where scenario.Priority > x && scenario.ProjectID == projectID
                orderby scenario.Priority
                select scenario;
            return topThird;

        }

        public IQueryable<Utility> GetTopThirdUtilities(long projectID)
        {
            //get max based on number of rows
            int x = (int)Math.Round((double)db.Scenarios.Where(s=> s.ProjectID == projectID).Distinct().Count() / 3.0); //set priority marker at 1/3

            //var q = db.Scenarios.Select( (p, index) => new {Position = index, p.Priority}).Where(p => p.Position == x);
            //var q = db.Scenarios.Select(p => new { p.Priority }).OrderBy(p => p.Priority).GroupBy(p => p.Priority).ElementAt(x);

            // Create the query.
            IQueryable<Utility> topThird =
                from utility in db.Utilities
                where utility.Scenario.Priority <= x && utility.Scenario.ProjectID == projectID
                orderby utility.Scenario.Priority
                select utility;
            return topThird;
        }

        public IQueryable<Utility> GetTopSixthUtilities(long projectID)
        {
            //get max based on number of rows
            int x = (int)Math.Round((double)db.Scenarios.Where(s => s.ProjectID == projectID).Distinct().Count() / 6.0); //set priority marker at 1/3

            //var q = db.Scenarios.Select( (p, index) => new {Position = index, p.Priority}).Where(p => p.Position == x);
            //var q = db.Scenarios.Select(p => new { p.Priority }).OrderBy(p => p.Priority).GroupBy(p => p.Priority).ElementAt(x);

            // Create the query.
            IQueryable<Utility> top =
                from utility in db.Utilities
                where utility.Scenario.Priority <= x && utility.Scenario.ProjectID == projectID
                orderby utility.Scenario.Priority
                select utility;
            return top;
        }

        //update progress
        public IQueryable<Step> Update()
        {//to be implemented
            return db.Steps;
        }



        //Persistence: 
        public void Save()
        {
            db.SubmitChanges();
        }

    }
}
