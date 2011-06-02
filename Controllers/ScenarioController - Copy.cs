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
        //IStepsRepository progressRepository;
        // Dependency Injection enabled constructors
        public ScenarioController()
            : this(new ScenarioRepository())
        {
        }

        public ScenarioController(IScenarioRepository repository)
        {
            scenarioRepository = repository;
        }

   
        // GET: /Scenarios/
        public virtual ActionResult ScenarioList()
        {
            var scenarios = scenarioRepository.GetAll();
            return View(scenarios);
        }

        // GET: /Scenarios/
        public virtual ActionResult Index()
        {
            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository);
            return View(vmodel);
        }


     

     //  [BypassAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult UpdatePriority(int[] priority) //int[] scenarios
        {
            Scenario s;
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
                    var stepsRepository = new StepsRepository();
                    stepsRepository.UpdateSteps();
                    stepsRepository.Save();
                }
                catch
                {//For Error w/o defined message
                    ModelState.AddModelError("ID", "Not Added Sucessfully");
                     result = new jr { success = "false" };  
                     return Json(result);  
                }//end catch
            }//else not valid 
            else ModelState.AddModelError("ID", "Record not Added Sucessfully");
            ModelState.Clear(); // this is the key, you could also just clear ModelState for the field
            var vmodel = ScenarioViewModel.CreateIndex(scenarioRepository);
            return View(vmodel);
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
        public virtual ActionResult Create()
        {
            Scenario scenario = new Scenario();
            return View(new ScenarioViewModel(scenario));
        }

        // POST: /Scenarios/Create/
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create(Scenario scenario)
        {
            scenario.DateAdded = DateTime.Now;
            scenario.LastModified = DateTime.Now;
            scenario.Name = scenario.Name.Trim();
            scenario.Description = scenario.Description.Trim();
    
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

        //// GET: /Scenarios/EditUtility/5  id=utility id
        //public virtual ActionResult EditUtility(int id)
        //{
        //    Scenario scenario = scenarioRepository.GetByID(id);
        //    if (scenario == null)
        //    {
        //        return View("NotFound");
        //    }
        //    return View(new ScenarioViewModel(scenario));
        //}


        ///// <summary>
        ///// Edits the specified id.
        ///// </summary>
        ///// <param name="id">The id.</param>
        ///// <param name="requirement">The requirement.</param>
        ///// <returns></returns>
        //// POST: /Utility/EditUtility/5 id=utility id
        //[AcceptVerbs(HttpVerbs.Post)]
        //public virtual ActionResult EditUtility(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        var scenarioToUpdate = scenarioRepository.GetByID(id);
        //        //UpdateModel(scenarioToUpdate, "Scenario");
        //        var utilityIDs = scenarioRepository.GetUtilitiesByScenarioID(id);
        //        int i = 0;//counter for collection obj
        //        foreach (var uid in utilityIDs)
        //        {
        //            Utility utilityToUpdate = scenarioRepository.GetUtilityByID((int)uid);
        //            foreach (var rObj in Request.Form)  //find descrtipion for util
        //            {
        //                //make sure form obj comparing to is utility id and id == utilityToUpdate
        //                if (Request.Form["Scenario.Utilities[" + i + "].ID"].Equals(utilityToUpdate.ID.ToString())
        //                    && rObj.Equals("Scenario.Utilities[" + i + "].ID")
        //                    )
        //                {   //update description
        //                    utilityToUpdate.Description = Request.Form["Scenario.Utilities[" + i + "].Description"];
        //                    i++; //goto next util obj
        //                }

        //                //utility.Description =  ut.Description;

        //                //UpdateModel(utility, "Utility");
        //            }//insert each Quality Attribute Response type for scenario
        //        }//insert each Quality Attribute Response type for scenario

        //        ModelState.AddModelErrors(scenarioToUpdate.GetRuleViolations());

        //        if (ModelState.IsValid)
        //        {
        //            scenarioRepository.Save();
        //            return RedirectToAction("EditUtility", new { id = scenarioToUpdate.ID });
        //        }
        //        else return View(new ScenarioViewModel(scenarioToUpdate));


        //    }//end try
        //    catch
        //    {
        //        return View("Error");
        //        // return View();
        //    }
        //}

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
        public virtual ActionResult ProgressBar()
        {
            var scenarios = scenarioRepository.Steps();
            return View(scenarios);

          
            //return View("ProgressBar");
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
