
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBAM.Helpers;
using CBAM.Models;

namespace CBAM.Controllers
{
    public class UtilityRatingController : Controller
    {
        IUtilityRepository utilityRepository;
        IScenarioRepository scenarioRepository;
        // Dependency Injection enabled constructors
    
        public UtilityRatingController()
            : this(new ScenarioRepository())
        {
        }
        public UtilityRatingController(IScenarioRepository repository)
        {
            scenarioRepository = repository;
        }

        public UtilityRatingController(IUtilityRepository repository)
        {
            utilityRepository = repository;
        }


        //for EditEntities get //top 1/6 to add util descriptions
        public ScenarioListUtilRatings populateScenatioList(long projID) //int[] scenarios
        {
            var utilModel = new UtilityRepository();
            var slist = new ScenarioListUtilRatings();
            slist.projectID = projID;
            var vmodel = ScenarioViewModel.CreateTopSixth(scenarioRepository, projID);

            slist.ScenariosForUtilRatingUpdate = new List<ScenarioForUtilRating>();
            var sToAdd = new ScenarioForUtilRating();

            //add items from view model
            foreach (var s in vmodel.ScenariosList)
            {
                slist.ScenariosForUtilRatingUpdate.Add(new ScenarioForUtilRating
                {
                    scenarioID = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    utilities = new List<utilRating>(),
                    QualityAttributeResponseTypes = utilModel.GetQualityAttributeResponseTypes().ToList(),
           
                });
                //order utilities according to QualityAttribute order
                var utils = s.Utilities.OrderBy(x => x.QualityAttributeResponseType.Order);
                foreach (var u in utils)
                {//Utilities to Scenario
                    //add utilities to last added scenario in ScenariosForUtilUpdate list
                    slist.ScenariosForUtilRatingUpdate[slist.ScenariosForUtilRatingUpdate.Count - 1]
                        .utilities
                        .Add(new utilRating
                        {
                            ID = u.ID,
                            Description = u.Description,
                            QualityAttributeResponseTypeType = u.QualityAttributeResponseType.Type,
                            Utility = u.Utility1,
                            Order = u.QualityAttributeResponseType.Order,
                        });
 
                        


                }//end util for loop
            }//end scenario for


            return slist;
        }


        // GET: /Scenarios/  //top 1/6 to add util rating
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
        public virtual ActionResult Edit(ScenarioListUtilRatings collection)
        {
            var sListUtilRating = collection.ScenariosForUtilRatingUpdate;
            var scenarioId = sListUtilRating.Select(x => x.scenarioID).FirstOrDefault();
            var scenario = scenarioRepository.GetByID((int)scenarioId);
            var projID = scenario.ProjectID;

            if (scenario == null)//projectID not found
            {
                //return to project page
            }
            try
            {
                int i = 0;
                foreach (var s in collection.ScenariosForUtilRatingUpdate)  //for each scenario
                {
                    foreach (var u in s.utilities)//for each util in that scenario
                    {   //get utility record
                        Utility utilityToUpdate = scenarioRepository.GetUtilityByID((int)u.ID);
                        //want to save valid entries even if ModelState is not valid
                        //User may not want to fill out entire form at once.
                        ModelState.AddModelErrors(u.GetRuleViolations());

                        if (u.Utility != null 
                            && utilityToUpdate.Utility1 != u.Utility)
                        {   //update description 
                            utilityToUpdate.Utility1 = u.Utility;
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
                   //ModelState.AddModelErrors(u.GetRuleViolations());
                    //added in populate list
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
