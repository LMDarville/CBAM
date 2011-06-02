using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBAM.Helpers;
using CBAM.Models;

namespace CBAM.Controllers
{
    public class VotesController : Controller
    {
        IScenarioRepository scenarioRepository;
        // Dependency Injection enabled constructors
        public VotesController()
            : this(new ScenarioRepository())
        {
        }
        public VotesController(IScenarioRepository repository)
        {
            scenarioRepository = repository;
        }

      

        //for Edit get //top 1/3 to edit Votes
        public ScenarioListForVotes populateScenatioListVotes(long projectID) //int[] scenarios
        {
            var vmodel = ScenarioViewModel.CreateTopThird(scenarioRepository, projectID);
            var slist = new ScenarioListForVotes();
            slist.ScenariosForVotes = new List<ScenarioForVotes>();
            slist.projectID = projectID;
            var sToAdd = new ScenarioForUtil();

            //add items from view model
            foreach (var s in vmodel.ScenariosList)
            {
                slist.ScenariosForVotes.Add(new ScenarioForVotes
                {
                    scenarioID = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    Votes = s.Votes == null ? 0 : s.Votes
                });
            }
            return slist;
        }
        

        // GET: /Scenarios/  //top 1/3 to add votes
        public virtual ActionResult Edit(long projID)
        {
            ModelStateHelpers.ModelMessage = null;
            var slist = populateScenatioListVotes(projID);  // return type ScenarioListForVotes
            return View(slist);
        }

        /// <summary>
        /// Edits the votes in list of scenarios
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit(ScenarioListForVotes collection)
        {
            var scenarioId = collection.ScenariosForVotes.Select(x => x.scenarioID).FirstOrDefault();
            var scenario = scenarioRepository.GetByID((int)scenarioId);
            var projID = scenario.ProjectID;

           try
            {
                int i = 0;
                foreach (var s in collection.ScenariosForVotes)  //for each scenario
                {
                    //get utility record
                    Scenario scenarioToUpdate = scenarioRepository.GetByID((int)s.scenarioID);
                    //want to save valid entries even if ModelState is not valid
                    //User may not want to fill out entire form at once.
                    if (s.Votes != null && scenarioToUpdate.Votes != s.Votes)
                    {   //update description 
                        scenarioToUpdate.Votes = s.Votes;
                        i++;
                    }
                }//end foreach scenario
                scenarioRepository.Save();
                ModelStateHelpers.ModelMessage = i + " Record(s) Saved Successfully";
              

                //update complete status on steps
                var stepsRepository = new StepsRepository();
                stepsRepository.UpdateSteps(projID);
                stepsRepository.Save();


                if (ModelState.IsValid)
                {
                    var slist = populateScenatioListVotes(projID);  // return type ScenarioListForVotes
                    return View(slist);
                    // Message = "Saved Successfully";
                }
                else
                {

                    ModelState.AddModelError("ID", "Record not Added Sucessfully");

                    //var vmodel = ScenarioViewModel.TopThird(scenarioRepository);
                    return View(collection);
                }

            }//end try
            catch
            {
                ModelState.AddModelError("ID", "Record not Added Sucessfully");
                //var vmodel = ScenarioViewModel.TopThird(scenarioRepository);
                return View(collection);
            }
        }


    }
}
