using System;
using System.Web;
using System.Web.Mvc;
using CBAM.Helpers;
using CBAM.Models;
using System.Collections.Generic;
using System.Threading;

namespace CBAM.Controllers
{
    public class ScenarioController : Controller
    {
        IScenarioRepository scenarioRepository;
        IStepsRepository stepsRepository;
        // Dependency Injection enabled constructors
        public ScenarioController()
            : this(new ScenarioRepository())
        {
        }

        public ScenarioController(IScenarioRepository repository)
        {
            scenarioRepository = repository;
            stepsRepository = new StepsRepository();
        }

        #region indices
        //each tab: When selected StepX action get step info
        //      progress and desctiption (accordion) for each tab
        //      if data is available, next action (ex "ScenarioList" is rendered)
        //      2nd action (ex "ScenarioList") gets the grid data 
 
        // GET: /Scenarios#tab-1
        public virtual ActionResult Step1(long projID)
        {
            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository, projID);
            return View(vmodel);
        }
        public virtual ActionResult ScenarioList(long projID)
        {
            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository, projID);
            var scenarios = vmodel.Scenarios;
            return PartialView(scenarios);
        }

        // GET: /Scenarios#tab-2
        public virtual ActionResult Step2(long projID)
        {
            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository, projID);
            return View(vmodel);
        }
        public virtual ActionResult TopThirdList(long projID)
        {
            var vmodel = ScenarioViewModel.CreateTopThird(scenarioRepository, projID);

            return PartialView(vmodel.TopScenarios);
        }

        // GET: /Scenarios#tab-3
        public virtual ActionResult Step3(long projID)
        {
            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository, projID);
            return View(vmodel);
        }
        public virtual ActionResult VotesList(long projID)
        {
            var vmodel = ScenarioViewModel.CreateTopThird(scenarioRepository, projID);

            return PartialView(vmodel.TopScenarios);
        }

        // GET: /Scenarios#tab-4
        public virtual ActionResult Step4(long projID)
        {
            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository, projID);
            return View(vmodel);
        }
        public virtual ActionResult TopSixthList(long projID)
        {
            var vmodel = ScenarioViewModel.CreateTopSixth(scenarioRepository, projID);

            return PartialView(vmodel.TopSixthScenarios);
        }

        //Get: /Scenario#tab-5
        public virtual ActionResult Step5(long projID)
        {
            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository, projID);
            return View(vmodel);
        }

        #endregion indices


        // GET: /Scenarios/
        public virtual ActionResult Index(long projID)
        {
            //update complete status on steps
            var stepsRepository = new StepsRepository();
            stepsRepository.UpdateSteps(projID);

            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository, projID);
            if (vmodel == null)
            {
                return View("NotFound"); 
            }

            return View(vmodel);
        }

     //  [BypassAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult UpdatePriority(int[] priority) //int[] scenarios
        {
            ModelState.Clear(); 

            //priority keeps coming in null, even though posted
            if (priority == null)
            {
                HttpContext ctx = System.Web.HttpContext.Current;
                string[] valArr = ctx.Request.Params.GetValues(0);
                Array.Resize(ref priority, valArr.Length);

                for (int i = 0; i < valArr.Length; i++)
                {
                    priority[i] = Convert.ToInt16(valArr[i]);
                }//end for
            }//end if
            
           

            Scenario s;
            s = scenarioRepository.GetByID(priority[1]);
            var result = new jr { success = "true" };
            if (ModelState.IsValid)
            {
                try
                {
                    for (int i = 0; i < priority.Length; i++)
                    {
                         s = scenarioRepository.GetByID(priority[i]);
                         s.Priority = i + 1;
                         scenarioRepository.Save();
                    }
                    //update complete status on steps
                    scenarioRepository.ClearVotesBottomTwoThirds(s.ProjectID);
                    var stepsRepository = new StepsRepository();
                    stepsRepository.UpdateSteps(s.ProjectID);
                    stepsRepository.Save();
                }
                catch
                {//For Error w/o defined message
                    ModelState.AddModelError("ID", "Not Added Sucessfully");
                    //var vmodelError = ScenarioViewModel.CreateIndex(scenarioRepository, s.ProjectID);
                    //return View(vmodelError);
                     result = new jr { success = "false" };  
                     return Json(result);  
                }//end catch
            }//else not valid 
            else ModelState.AddModelError("ID", "Record not Added Sucessfully");
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the field

            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository, s.Project.ID);
            var scenarios = vmodel.Scenarios;
            return PartialView("ScenarioList", scenarios);
 
            //return View(vmodel);
            //return Action (Index);
        }

        [Serializable]  
        public class jr  //jquery result
        {  
            public string success { get; set; }  
        }  

        // GET: /Scenario/Detail/5
        public virtual ActionResult Detail(int? id)
        {
            if (id == null){
                return View("NotFound"); 
            }

            Scenario scenario = scenarioRepository.GetByID(id.Value);
            if (scenario == null){
                return View("NotFound"); 
            }

            return View(new ScenarioViewModel(scenario));
        }

        // GET: /Scenarios/Edit/5
        public virtual ActionResult Edit(int id)
        {
            Scenario scenario = scenarioRepository.GetByID(id);
            if (scenario == null){
                return View("NotFound");
            }
            return View(new ScenarioViewModel(scenario));
        }
             
        // POST: /Scenarios/Edit/5
        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            Scenario scenario = scenarioRepository.GetByID(id);
            UpdateModel(scenario, "Scenario");
            scenario.LastModified = DateTime.Now;
            scenario.Name = scenario.Name.Trim();
            scenario.Description = scenario.Description.Trim();
            //scenario.ImportanceRatingID = Request.Form["ImportanceRatingID"]; //update not working on lookup
            ModelState.AddModelErrors(scenario.GetRuleViolations());

            if (ModelState.IsValid)
            {
                scenarioRepository.Save();
                return RedirectToAction("Detail", new { id = scenario.ID });
            }
            else return View(new ScenarioViewModel(scenario));

          
        }
     
        // GET: /Scenarios/Create
        public virtual ActionResult Create(long projID)
        {
            Project p = scenarioRepository.GetProjectByID(projID);
            Scenario scenario = new Scenario();
            scenario.ProjectID = projID;
            scenario.Project = p;
            return View(new ScenarioViewModel(scenario));
        }

        // POST: /Scenarios/Create/
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create(Scenario scenario, long projID)
        {
            Project p = scenarioRepository.GetProjectByID(projID);
            scenario.DateAdded = DateTime.Now;
            scenario.LastModified = DateTime.Now;
            scenario.IsActive = true;
            scenario.Name = scenario.Name.Trim();
            scenario.Description = scenario.Description.Trim();
            scenario.ProjectID = projID;
            scenario.Project = p;
    
            ModelState.AddModelErrors(scenario.GetRuleViolations());
            if (ModelState.IsValid)
            {
                try
                {
                    scenarioRepository.Add(scenario);
                    scenarioRepository.Save();
                    scenarioRepository.addScenarioToUtility(scenario.ID);
                    scenarioRepository.Save();

                    //update priority, add new as 1st priority
                    var priotity = new ScenarioPriorityList().getPriorityList();
                    UpdatePriority(priotity);
                   
              return RedirectToAction("Detail", new { id = scenario.ID });
                }
                catch
                {
                    //For Error w/o defined message
                    ModelState.AddModelError("ID", "Record not Added Sucessfully");
                    //ModelState.AddModelErrors(scenario.GetRuleViolations());
                    return View(new ScenarioViewModel(scenario));
                }
            }
            //else not valid 
            else ModelState.AddModelError("ID", "Record not Added Sucessfully");
            return View(new ScenarioViewModel(scenario));
        }//end create post

        protected override void HandleUnknownAction(string actionName)
        {
            throw new HttpException(404, "Action not found");
        }

        public virtual ActionResult Confused()
        {
            return View();
        }

        /// <summary>
        /// Progress Bar
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult ProgressBar(long projectID)
        {
            var viewmodel = StepsViewModel.CreateIndex(stepsRepository, projectID);
            return View(viewmodel);
        }

        //Get:  /Shared/Step/4
        public virtual ActionResult Step(int id, long projID)
        {
            var vmodel = StepsViewModel.getStep(stepsRepository, id, projID);
            return View(vmodel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult GetText()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
            }
            return Json("All finished!");
        }


        /// <summary>
        /// ///////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Trouble()
        {
            return View("Error");
        }
    }
}
