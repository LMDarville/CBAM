using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBAM.Helpers;
using CBAM.Models;

namespace CBAM.Controllers
{
    public class UtilityController : Controller
    {
        IUtilityRepository utilityRepository;
        IScenarioRepository scenarioRepository;
        // Dependency Injection enabled constructors
    
        public UtilityController()
            : this(new ScenarioRepository())
        {
        }
        public UtilityController(IScenarioRepository repository)
        {
            scenarioRepository = repository;
        }
      
        public UtilityController(IUtilityRepository repository)
        {
            utilityRepository = repository;
        }


        //Get: /Utility/Edit?projID=1     top 1/3 to add util descriptions-
        public ScenarioList populateScenatioList(long projectID) //int[] scenarios
        {
            var slist = new ScenarioList();
            var vmodel = ScenarioViewModel.CreateTopThird(scenarioRepository, projectID);
            slist.ScenariosForUtilUpdate = new List<ScenarioForUtil>();
            slist.projectID = projectID;
            var sToAdd = new ScenarioForUtil();

            //add items from view model
            foreach (var s in vmodel.ScenariosList)
            {
                slist.ScenariosForUtilUpdate.Add(new ScenarioForUtil
                {
                    scenarioID = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    utilities = new List<util>(),
                });
                //order utilities according to QualityAttribute order
                var utils = s.Utilities.OrderBy(x => x.QualityAttributeResponseType.Order);
                foreach (var u in utils)
                {//Utilities to Scenario
                    //add utilities to last added scenario in ScenariosForUtilUpdate list
                    slist.ScenariosForUtilUpdate[slist.ScenariosForUtilUpdate.Count - 1].utilities.Add(new util 
                    { 
                        ID = u.ID, 
                        Description = u.Description, 
                        QualityAttributeResponseTypeType = u.QualityAttributeResponseType.Type 
                    });
                }//end util for loop
            }//end scenario for

            return slist;
        }


        // GET: /Scenarios/  //top 1/3 to add util descriptions
        public virtual ActionResult Edit(long projID)
        {
            ModelStateHelpers.ModelMessage = null;
            var slist = populateScenatioList(projID);
            return View(slist);
        }


        /// <summary>
        /// Edits the utilities in list of scenarios
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit(ScenarioList collection)
        {
            var scenarioId = collection.ScenariosForUtilUpdate.Select(x => x.scenarioID).FirstOrDefault();
            var scenario = scenarioRepository.GetByID((int)scenarioId);
            var projID = scenario.ProjectID;
           
            try
            {
                int i = 0;
                foreach (var s in collection.ScenariosForUtilUpdate)  //for each scenario
                {
                    foreach (var u in s.utilities)//for each util in that scenario
                    {   //get utility record
                        Utility utilityToUpdate = scenarioRepository.GetUtilityByID((int)u.ID);
                        //want to save valid entries even if ModelState is not valid
                        //User may not want to fill out entire form at once.
                        if (u.Description != null && u.Description.Length < 250
                            && utilityToUpdate.Description != u.Description)
                        {   //update description 
                            utilityToUpdate.Description = u.Description;
                            i++;
                        }
                    }//end foreach utility
                }//end foreach scenario
                scenarioRepository.Save();
                ModelStateHelpers.ModelMessage = i + " Record(s) Saved Successfully";
                
                //update complete status on steps
                var stepsRepository = new StepsRepository();
                stepsRepository.UpdateSteps(projID);

                if (ModelState.IsValid)
                {
                    var slist = populateScenatioList(projID);
                    return View(slist);
                    // Message = "Saved Successfully";
                }
                else
                {
                    ModelState.AddModelError("ID", "Record not Added Sucessfully");
                    var slist = populateScenatioList(projID);
                    return View(slist);
                }

            }//end try
            catch
            {
                ModelState.AddModelError("ID", "Record not Added Sucessfully");
                var slist = populateScenatioList(projID);
                return View(slist);
            }
        }

      

      

    }
}
