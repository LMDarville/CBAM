using System.Web.Mvc;
using CBAM.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace CBAM.Models
{
    public class ScenarioPriorityList
    {
        public CBAMDataContext db = new CBAMDataContext();
        public ScenarioPriorityList()
        {//constructor
        }
        public int[] getPriorityList()
        {
            var priorityList = 
            from scenario in db.Scenarios.OrderBy(x => x.Priority)
                select (int)scenario.ID;
            return priorityList.ToArray();
        }
    }
    public class ScenarioViewModel
    {
        //for detail
        public Scenario Scenario { get; private set; }
        public SelectList ImportanceList { get; private set; }
        public SelectList diff{ get; private set; }

        //for index
        public IQueryable<Scenario> Scenarios { get; set; }
        public IList<Step> Steps { get; set; }
        public Step nextStepToComplete { get; set; }
        public long projectID { get; set; }
        public string ProjectName { get; set; }

        //for utility
        public Utility Utility { get; private set; }

        //for TopThird, Topsixth
        public IQueryable<Utility> TopUtilities { get; set; }
        public List<Utility> UtilitiesList { get; set; }
        public List<Scenario> ScenariosList { get; set; }
        public IQueryable<Scenario> TopScenarios { get; set; }
        public IQueryable<Scenario> TopSixthScenarios { get; set; }
        
        public CBAMDataContext db = new CBAMDataContext();


        public ScenarioViewModel()
        {
        }

        public ScenarioViewModel(Scenario scenario)
        {
            Scenario = scenario;
            //  Importances  = new SelectList(db., dinner.Country);
            ImportanceList = new SelectList(db.Importances, "IDRatingDescription", "Description",scenario.ImportanceRatingID);
        }

       
        public ScenarioViewModel (Utility utility)
        {
            Utility = utility;
        }


        public static ScenarioViewModel CreateIndex(IScenarioRepository repository, long projectID)
        {
            var viewModel = new ScenarioViewModel();
                viewModel.projectID = projectID;
                viewModel.Scenarios = repository.GetByProjectID(projectID).OrderBy(x => x.Priority);
                viewModel.Steps = repository.Steps().ToList();
                viewModel.ProjectName = repository.GetProjectByID(projectID).Name;
                viewModel.nextStepToComplete = repository.NextStepToComplete();
            return viewModel;
        }




        public static ScenarioViewModel CreateTopThird(IScenarioRepository repository, long projectID)
        {
            var viewModel = new ScenarioViewModel();
            viewModel.UtilitiesList = repository.UtilityList(projectID).OrderBy(x => x.Scenario.Priority).ToList();
            viewModel.ScenariosList = repository.GetTopThird(projectID).OrderBy(x => x.Priority).ToList();
            viewModel.TopScenarios = repository.GetTopThird(projectID).OrderBy(x => x.Priority);
            return viewModel;
        }

        public static ScenarioViewModel CreateTopSixth(IScenarioRepository repository, long projectID)
        {
            var viewModel = new ScenarioViewModel();
            viewModel.ScenariosList = repository.GetTopSixth(projectID).OrderBy(x => x.Priority).ToList();
            viewModel.TopSixthScenarios = repository.GetTopSixth(projectID).OrderBy(x => x.Priority);
             return viewModel;
        }



     

    }//end class ScenarioViewModel 

   
}//end namespace


