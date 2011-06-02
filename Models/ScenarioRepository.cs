using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBAM.Models
{
    public class ScenarioRepository: CBAM.Models.IScenarioRepository {

        private CBAMDataContext db = new CBAMDataContext();
        
        //Query Methods
        //public IQueryable<Scenario> GetAll(){
        //    return db.Scenarios;
        //}
        public IQueryable<Scenario> GetByProjectID(long projectID)
        {
           // return db.Scenarios.Where(x => x.ProjectID == projectID).Where(x => x.IsActive);
            IQueryable <Scenario> scenarios =
               from s in db.Scenarios
               where s.ProjectID == projectID && s.IsActive
               select s;

            return scenarios;
        }

        public IQueryable<Step> Steps()
        {
            return db.Steps;
        }

        public IQueryable<Utility> UtilityList(long projectID)
        {
            return db.Utilities.Where(x => x.Scenario.ProjectID == projectID);
        }

        //public IQueryable<QualityAttributeResponseType> UtilityList1(long projectID)
        //{
        //    return db.QualityAttributeResponseTypes;
        //}

        public ExpectedUtility Get_ExpectedUtility(long stratID, long scenID)
        {
            ExpectedUtility item = 
                (from p in db.ExpectedUtilities
                    where (p.ArchitecturalStrategyID == stratID &&
                            p.ScenarioID == scenID)
                    select p).SingleOrDefault();
            return item;
        }

        public Step NextStepToComplete()
        {
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
            else {
                nextStep = incompleteSteps.First();
            }

            return nextStep;
        }

        public IQueryable<QualityAttributeResponseType> QualityAttributeResponseTypeList()
        {
            IQueryable<QualityAttributeResponseType> qarType =
               from qar in db.QualityAttributeResponseTypes
               orderby qar.Order
               select qar;
            return qarType;

        }

        public IQueryable<Scenario> ScenarioList(long projectID)
        {
            return db.Scenarios.Where(x => x.ProjectID == projectID).Where(x => x.IsActive);
        }
      
        public IEnumerable <Importance> ImportanceList()
        {
            return db.Importances;
        }

        public Scenario GetByID(int ID){
            return db.Scenarios.SingleOrDefault(s => s.ID == ID);
        }

        public Project GetProjectByID(long id){
            return db.Projects.SingleOrDefault(x => x.ID == id);
        }

        public IQueryable<long> GetUtilitiesByScenarioID(int id)
        {
            IQueryable<long> ids =
                from utility in db.Utilities
                where utility.Scenario.ID == id
                select utility.ID;
            return ids;
        }

        public Utility GetUtilityByID(int id)
        {
            return db.Utilities.SingleOrDefault(s => s.ID == id);
        }

        //Insert/Delete
        public void Add(Scenario scenario){
            scenario.Votes = 0;
            db.Scenarios.InsertOnSubmit(scenario);
            //need to add scenario to utility table after saving
        }

        public void addScenarioToUtility(long scenarioID){
            foreach (var qaType in db.QualityAttributeResponseTypes)
            {
               var utility =  new Utility();
                utility.QualityAttributeResponseTypeID = qaType.ID;
                utility.ScenarioID = scenarioID;
                utility.Description = ""; //won't accept null for some reason
                db.Utilities.InsertOnSubmit(utility);
            }//insert each Quality Attribute Response type for scenario
                //to be defined later by user
        }

        public IQueryable<Utility> GetTopThirdUtilities(long projectID)
        {
            var scenarios = ScenarioList(projectID);
            //get max based on number of rows
            int x = (int)Math.Round((double)scenarios.Distinct().Count() / 3.0); //set priority marker at 1/3
             
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

        public IQueryable<Scenario> GetTopThird(long projectID)
        {
            var scenarios = ScenarioList(projectID);
        
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
        public IQueryable<Scenario> GetTopSixth(long projectID)
        {
            var scenarios = ScenarioList(projectID);
    
            //get max based on number of rows
            int x = (int)Math.Round((double)db.Scenarios.Where(s => s.ProjectID == projectID).Distinct().Count() / 6.0); //set priority marker at 1/3

            //var q = db.Scenarios.Select( (p, index) => new {Position = index, p.Priority}).Where(p => p.Position == x);
            //var q = db.Scenarios.Select(p => new { p.Priority }).OrderBy(p => p.Priority).GroupBy(p => p.Priority).ElementAt(x);

            // Create the query.
            IQueryable<Scenario> top =
                from s in db.Scenarios
                where s.Priority <= x && s.ProjectID == projectID
                orderby s.Votes descending, s.Priority
                select s;

         
           return top;

          }

        public List<Scenario> GetTopThirdList(long projectID)
            {
                return GetTopThird(projectID).ToList();
            }


        public void ClearVotesBottomTwoThirds(long projectID)
        {
            var s = GetBottomTwoTopThirds(projectID);

            foreach (var v in s)
            {
                v.Votes = 0;
            }
            db.SubmitChanges();

        }
         

        public void Delete(Scenario scenario)
        {
            db.Scenarios.DeleteOnSubmit(scenario);
        }

        //Persistence
        public void Save()
        {
            db.SubmitChanges();
        }

    }
}
