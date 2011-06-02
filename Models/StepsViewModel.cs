using System.Web.Mvc;
using CBAM.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace CBAM.Models
{
    public class StepsViewModel
    {
        //for index
        public IList<Step> Steps { get; set; }
        public Step nextStepToComplete { get; set; }
        public Step thisStep { get; set; }

        //for update
        public IList<Scenario> ScenarioList { get; set; }
        public long projectID;

        public CBAMDataContext db = new CBAMDataContext();

        public StepsViewModel()
        {
        }

        public static StepsViewModel CreateIndex(IStepsRepository repository, long projectID)
        {
            var viewModel = new StepsViewModel();
            viewModel.Steps = repository.GetAll().ToList();
            viewModel.nextStepToComplete = repository.GetNextStepToComplete(projectID);
            return viewModel;
        }

        public static StepsViewModel getStep(IStepsRepository repository, int mystep, long projID)
        {
            var viewModel = new StepsViewModel();
            viewModel.Steps = repository.GetAll().ToList();
            viewModel.nextStepToComplete = repository.GetNextStepToComplete(projID);
            viewModel.thisStep = repository.GetByStepID(mystep);
            viewModel.projectID = projID;
            return viewModel;
        }

       


        public static StepsViewModel BottomTwoTopThirds(IStepsRepository repository, long projectID)
        {
            var viewModel = new StepsViewModel();
            viewModel.ScenarioList = repository.GetBottomTwoTopThirds(projectID).OrderBy(x => x.Priority).ToList();
            return viewModel;
        }
    }
}
